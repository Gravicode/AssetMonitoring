using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class DeviceRequestService : ICrud<DeviceRequest>
    {
        AssetMonitoringDB db;

        public DeviceRequestService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.DeviceRequests.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.DeviceRequests.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<DeviceRequest> FindByKeyword(string Keyword)
        {
            var data = from x in db.DeviceRequests
                       where x.PicName.Contains(Keyword) || x.MerchantName.Contains(Keyword) || x.SerialNo.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<DeviceRequest> GetAllData()
        {
            return db.DeviceRequests.Include(x=>x.Merchant).Include(x => x.Salesman).ToList();
        }

        public DeviceRequest GetDataById(object Id)
        {
            return db.DeviceRequests.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(DeviceRequest data)
        {
            try
            {
                db.DeviceRequests.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(DeviceRequest data)
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
            return db.DeviceRequests.Max(x => x.Id);
        }
    }

}