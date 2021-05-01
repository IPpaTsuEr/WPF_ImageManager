using ImageManager.Modle;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.Helper
{
    class DataBaseHelper
    {
        SqliteConnection dbc;
        SqliteCommand cmd;

        bool NeedCreate = false;
        string dbpath;

        private void Create() { 
            cmd.CommandText = "create table ImageLibrary(path TEXT)";
            var result = cmd.ExecuteNonQuery();
            if (result == -1) Trace.WriteLine("创建Lib表失败");
            cmd.CommandText = "create table ImageInfo(id INTEGER PRIMARY KEY AUTOINCREMENT,data TEXT)";
            result = cmd.ExecuteNonQuery();
            if (result == -1) Trace.WriteLine("创建info表失败");
        }

        public DataBaseHelper(string StoragePath)
        {
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(StoragePath))) 
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(StoragePath));
            if (!File.Exists(StoragePath))
            {
                File.Create(StoragePath).Close();
                NeedCreate = true;
            }

            dbc = new SqliteConnection($"Data Source=\"{StoragePath}\"");
            dbc.Open();
            if(dbc.State == System.Data.ConnectionState.Open)
            {
                cmd = dbc.CreateCommand();
            }
            if (NeedCreate) Create();
            dbpath = StoragePath;
        }

        public void Reset()
        {
            if (dbc.State != System.Data.ConnectionState.Closed)
            {
                cmd.Dispose();
                dbc.Close();
            }
            if (File.Exists(dbpath)) File.Delete(dbpath);
            File.Create(dbpath).Close();
            dbc = new SqliteConnection($"Data Source=\"{dbpath}\"");
            dbc.Open();
            if (dbc.State == System.Data.ConnectionState.Open)
            {
                cmd = dbc.CreateCommand();
            }
            Create();
        }

        public void AddLibrary(string path)
        {
            cmd.CommandText =$"insert into ImageLibrary values(\"{path}\")";
            var result = cmd.ExecuteNonQuery();
            if (result == -1) Trace.WriteLine("插入Lib表失败");
        }
        public void RemoveLibrary(string path)
        {
            cmd.CommandText =$"delete from ImageLibrary where path=\"{path}\"";
            var result = cmd.ExecuteNonQuery();
            if (result == -1) Trace.WriteLine("删除Lib表失败");
        }

        public bool AddImage(ImageInfos infos)
        {
            var data = JsonHelper.Convert(infos);
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }
            cmd.CommandText = $"insert into ImageInfo(data) values('{data}')";
            var result = cmd.ExecuteNonQuery();
            if (result == -1)
            {
                Trace.WriteLine("插入Image表失败");
                return false;
            }
            cmd.CommandText = "SELECT last_insert_rowid()";
            infos.ID = (long)cmd.ExecuteScalar();
            return true;
        }

        public bool RemoveImage(ImageInfos infos)
        {
            var data = JsonHelper.Convert(infos);
            if (infos.ID == -1 || string.IsNullOrWhiteSpace(data))
            {
                Trace.WriteLine("移除Image表失败:对象未入库");
                return false;
            }
            cmd.CommandText = $"delete from ImageInfo where id = {infos.ID}";
            var result = cmd.ExecuteNonQuery();
            if (result == -1)
            {
                Trace.WriteLine("移除Image表失败");
                return false;
            }
            return true;
        } 

        public bool BatchRmoveImage(List<ImageInfos> data)
        {
            var dt = dbc.BeginTransaction();
            var dc = new SqliteCommand("delete from ImageInfo where id = @ID",dbc,dt);
            try
            {
                foreach (var item in data)
                {
                    dc.Parameters.Add(new SqliteParameter("ID",item.ID));
                    dc.ExecuteNonQuery();
                    dc.Parameters.Clear();
                }
                dt.Commit();
            }catch(Exception e)
            {
                dt.Rollback();
                Console.WriteLine($"Batch Delete Error : {e.Message}");
                return false;
            }
            return true;
        }

        public bool UpdateImage(ImageInfos infos)
        {
            
            var data = JsonHelper.Convert(infos);
            if (infos.ID == -1 || string.IsNullOrWhiteSpace(data))
            {
                Trace.WriteLine("更新Image表失败:对象未入库");
                return false;
            }
            cmd.CommandText = $"update ImageInfo set data='{data}' where id = {infos.ID}";
            var result = cmd.ExecuteNonQuery();
            if (result == -1)
            {
                Trace.WriteLine("更新Image表失败");
                return false;
            }
            return true;
        }

        public void ReadAllLib(ICollection<LibraryInfos> lib)
        {
            cmd.CommandText = $"select * from ImageLibrary";
            var result = cmd.ExecuteReader();
            while (result.Read())
            {
               var data =  result.GetString(0);
                if (!string.IsNullOrWhiteSpace(data))
                    lib.Add(new LibraryInfos() { Name = System.IO.Path.GetFileName(data), Path=data, IsValid=Directory.Exists(data)});
                else
                {
                    Trace.WriteLine($"读取lib错误");
                }
            }
            result.Close();
        }

        public void ReadAllImage(ICollection<ImageInfos> lis)
        {
            cmd.CommandText = $"select * from ImageInfo";
            var result = cmd.ExecuteReader();
            List<ImageInfos> cache = new List<ImageInfos>(); 
            while (result.Read())
            {
                var id = result.GetInt64(0);
                var data = result.GetString(1);

                if (!string.IsNullOrWhiteSpace(data))
                {
                    var imif = JsonHelper.Convert(data);
                    if (imif != null)
                    {
                        imif.ID = id;
                        cache.Add(imif);
                    }
                }
                else
                {
                    Trace.WriteLine($"读取image错误");
                }
            }
            result.Close();
            if(cache.Count > 0)
            {
                App.Current.Dispatcher.Invoke(()=> { 
                    foreach (var item in cache)
                    {
                        lis.Add(item);
                    }                
                }); 
            }
        }
    }
}
