using AssetMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using AssetMonitoring.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitoring.Web.Data
{
    public class MerchantService : ICrud<Merchant>
    {
        AssetMonitoringDB db;

        public MerchantService()
        {
            if (db == null) db = new AssetMonitoringDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Merchants.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Merchants.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Merchant> FindByKeyword(string Keyword)
        {
            var data = from x in db.Merchants
                       where x.Nama.Contains(Keyword) || x.Keterangan.Contains(Keyword) || x.Lokasi.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Merchant> GetAllData()
        {
            return db.Merchants.ToList();
        }

        public Merchant GetDataById(object Id)
        {
            return db.Merchants.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Merchant data)
        {
            try
            {
                db.Merchants.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Merchant data)
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
            return db.Merchants.Max(x => x.Id);
        }
    }

}