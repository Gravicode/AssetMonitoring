using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class DeviceService : ICrud<Device>
    {
        AssetMonitoringDB db;

        public DeviceService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Devices.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Devices.Remove(selData);
            db.SaveChanges();
            return true;
        }
        
        public List<Device> FindByKeyword(string Keyword)
        {
            var data = from x in db.Devices.Include(c=>c.Warehouse)
                       where (!string.IsNullOrEmpty(x.Longitude) && !string.IsNullOrEmpty(x.Latitude)) && x.Nama.Contains(Keyword) || x.Jenis.Contains(Keyword) || x.Lokasi.Contains(Keyword) || x.Merek.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Device> GetAllData()
        {
            return db.Devices.ToList();
        } 
        
        public List<Device> GetReadyDevice()
        {
            return db.Devices.Where(x=>x.IsAvailable).ToList();
        }  
        
        public List<Device> GetBrokenDevice()
        {
            return db.Devices.Where(x=> x.Status == DeviceConditions.Baik || x.Status == DeviceConditions.Rusak || x.Status == DeviceConditions.Perbaikan).ToList();
        }

        public Device GetDataById(object Id)
        {
            return db.Devices.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Device data)
        {
            try
            {
                db.Devices.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Device data)
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
            return db.Devices.Max(x => x.Id);
        }
    }

}