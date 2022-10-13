using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class DeviceHistoryService : ICrud<DeviceHistory>
    {
        AssetMonitoringDB db;

        public DeviceHistoryService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.DeviceHistorys.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.DeviceHistorys.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<DeviceHistory> FindByKeyword(string Keyword)
        {
            var data = from x in db.DeviceHistorys
                       where x.Deskripsi.Contains(Keyword) || x.Data.Contains(Keyword) || x.SerialNo.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<DeviceHistory> GetAllData()
        {
            return db.DeviceHistorys.ToList();
        }

        public DeviceHistory GetDataById(object Id)
        {
            return db.DeviceHistorys.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(DeviceHistory data)
        {
            try
            {
                db.DeviceHistorys.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(DeviceHistory data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.DeviceHistorys.Max(x => x.Id);
        }
    }

}