using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AssetMonitoring.Models
{
    #region auth
    [DataContract]
    public class AuthenticationModel
    {
        [DataMember(Order = 1)]
        public string ApiKey { get; set; }
    }
    [DataContract]
    public class AuthenticationUserModel
    {
        [DataMember(Order = 1)]
        public string Username { get; set; }
        [DataMember(Order = 2)]
        public string Password { get; set; }
    }
    [DataContract]
    public class AuthenticatedUserModel
    {
        [DataMember(Order = 1)]
        public string Username { get; set; }
        [DataMember(Order = 2)]
        public string AccessToken { get; set; }
        [DataMember(Order = 3)]
        public string TokenType { get; set; }
        [DataMember(Order = 4)]
        public DateTime? ExpiredDate { get; set; }
    }
    #endregion
    #region GRPC
    [ServiceContract]
    public interface IAuth
    {
        [OperationContract]
        Task<AuthenticatedUserModel> AuthenticateWithUsername(AuthenticationUserModel data, CallContext context = default);

        [OperationContract]
        Task<AuthenticatedUserModel> AuthenticateWithApiKey(AuthenticationModel data, CallContext context = default);
    }
    [ServiceContract]
    public interface IDevice : ICrudGrpc<Device>
    {

    }
  
    [ServiceContract]
    public interface IDeviceRequest : ICrudGrpc<DeviceRequest>
    {

    }
    [ServiceContract]
    public interface IDeviceHistory : ICrudGrpc<DeviceHistory>
    {

    }

    [ServiceContract]
    public interface IDeviceRepair : ICrudGrpc<DeviceRepair>
    {

    }
    [ServiceContract]
    public interface IMerchant : ICrudGrpc<Merchant>
    {

    }
    [ServiceContract]
    public interface IWorkshop : ICrudGrpc<Workshop>
    {

    }

    [ServiceContract]
    public interface IWarehouse : ICrudGrpc<Warehouse>
    {

    }

    [ServiceContract]
    public interface IUserProfile : ICrudGrpc<UserProfile>
    {
        [OperationContract]
        Task<UserProfile> GetItemByEmail(InputCls input, CallContext context = default);

        [OperationContract]
        Task<UserProfile> GetItemByPhone(InputCls input, CallContext context = default);

        [OperationContract]
        Task<OutputCls> IsUserExists(InputCls input, CallContext context = default);

        [OperationContract]
        Task<OutputCls> GetUserRole(InputCls input, CallContext context = default);
    }
    #endregion

    #region Common
    public interface ICrud<T> where T : class
    {
        Task<bool> InsertData(T data);
        Task<bool> UpdateData(T data);
        Task<List<T>> GetAllData();
        Task<T> GetDataById(long Id);
        Task<bool> DeleteData(long Id);
        Task<long> GetLastId();
        Task<List<T>> FindByKeyword(string Keyword);
    }
    [ServiceContract]
    public interface ICrudGrpc<T> where T : class
    {
        [OperationContract]
        Task<OutputCls> InsertData(T data, CallContext context = default);
        [OperationContract]
        Task<OutputCls> UpdateData(T data, CallContext context = default);
        [OperationContract]
        Task<List<T>> GetAllData(CallContext context = default);
        [OperationContract]
        Task<T> GetDataById(InputCls Id, CallContext context = default);
        [OperationContract]
        Task<OutputCls> DeleteData(InputCls Id, CallContext context = default);
        [OperationContract]
        Task<OutputCls> GetLastId(CallContext context = default);
        [OperationContract]
        Task<List<T>> FindByKeyword(string Keyword, CallContext context = default);
    }
    [DataContract]
    public class InputCls
    {
        [DataMember(Order = 1)]
        public string[] Param { get; set; }
        [DataMember(Order = 2)]
        public Type[] ParamType { get; set; }
    }
    [DataContract]
    public class OutputCls
    {
        [DataMember(Order = 1)]
        public bool Result { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
        [DataMember(Order = 3)]
        public string Data { get; set; }
    }
    #endregion
    #region database

    [DataContract]
    public class Warehouse
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [DataMember(Order = 2)]
        [Required]
        public string KodeWarehouse { get; set; }
        [DataMember(Order = 3)]
        public string? Nama { get; set; }

        [DataMember(Order = 4)]
        public string? Pic { get; set; }

        [DataMember(Order = 5)]
        public string? NoHpPic { get; set; }
        [DataMember(Order = 6)]
        public string? Lokasi { get; set; }
        [DataMember(Order = 7)]
        public string? Keterangan { get; set; }
        [DataMember(Order = 8)]
        public string? Latitude { get; set; }
        [DataMember(Order = 9)]
        public string? Longitude { get; set; }
        public ICollection<Device> Devices { get; set; }

    } 
    [DataContract]
    public class Workshop
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [DataMember(Order = 2)]
        [Required]
        public string KodeWorkshop { get; set; }
        [DataMember(Order = 3)]
        public string? Nama { get; set; }

        [DataMember(Order = 4)]
        public string? Pic { get; set; }

        [DataMember(Order = 5)]
        public string? NoHpPic { get; set; }
        [DataMember(Order = 6)]
        public string? Lokasi { get; set; }
        [DataMember(Order = 7)]
        public string? Keterangan { get; set; }

        [DataMember(Order = 8)]
        public string? Latitude { get; set; }
        [DataMember(Order = 9)]
        public string? Longitude { get; set; }

        public ICollection<DeviceRepair> DeviceRepairs { get; set; }
    }
    [DataContract]
    public class Merchant
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [DataMember(Order = 2)]
        [Required]
        public string KodeMerchant { get; set; }
        [DataMember(Order = 3)]
        public string? Nama { get; set; }

        [DataMember(Order = 4)]
        public string? Pic { get; set; }

        [DataMember(Order = 5)]
        public string? NoHpPic { get; set; }
        [DataMember(Order = 6)]
        public string? Lokasi { get; set; }
        [DataMember(Order = 7)]
        public string? Keterangan { get; set; }

        [DataMember(Order = 8)]
        public string? Latitude { get; set; }
        [DataMember(Order = 9)]
        public string? Longitude { get; set; }
        public ICollection<DeviceRequest> DeviceRequests { get; set; }
        public ICollection<DeviceRepair> DeviceRepairs { get; set; }
        
    }

    [DataContract]
    public class Device
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        [Required]        
        public string SerialNo { get; set; }

        [DataMember(Order = 3)]
        public string? Nama { get; set; }

        [DataMember(Order = 4)]
        public string? Jenis { get; set; }

        [DataMember(Order = 5)]
        public string? Merek { get; set; }

        [DataMember(Order = 6)]
        public DateTime? TanggalPasang { get; set; }

        [DataMember(Order = 7)]
        public DateTime? TanggalProduksi { get; set; }
        [DataMember(Order = 8)]
        public string? Lokasi { get; set; }
        [DataMember(Order = 9)]
        public DeviceConditions Status { get; set; }
        [DataMember(Order = 10)]
        public string? Latitude { get; set; }
        [DataMember(Order = 11)]
        public string? Longitude { get; set; }
        [DataMember(Order = 12)]
        public bool IsAvailable { get; set; } = true;

        public virtual Warehouse Warehouse { set; get; }
        [ForeignKey("Warehouse")]
        public long? WarehouseId { get; set; }

    }
    public enum DeviceConditions { Baik, Rusak, Perbaikan, Hilang }

    [DataContract]
    public class DeviceHistory
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        [Required]
        public string SerialNo { get; set; }

        [DataMember(Order = 3)]
        public string HistoryType { get; set; }

        [DataMember(Order = 4)]
        public string? Data { get; set; }

        [DataMember(Order = 5)]
        public DateTime? EventDate { get; set; }

        [DataMember(Order = 6)]
        public string? Deskripsi { get; set; }
        [DataMember(Order = 7)]
        public string? Latitude { get; set; }
        [DataMember(Order = 8)]
        public string? Longitude { get; set; }
    }

    [DataContract]
    public class DeviceRequest
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        [Required]
        public string SerialNo { get; set; }

        [DataMember(Order = 3)]
        public DateTime? RequestDate { get; set; }

        [DataMember(Order = 4)]
        public string? PicName { get; set; }
        
        [DataMember(Order = 5)]
        public string? MerchantName { get; set; }
        
        [DataMember(Order = 6)]
        public string Status { get; set; }
        [DataMember(Order = 7)]
        public string Keterangan { get; set; }

        public virtual UserProfile Salesman { set; get; }
        [ForeignKey("UserProfile")]
        public long? UserProfileId { get; set; }

        public virtual Merchant Merchant { set; get; }
        [ForeignKey("Merchant")]
        public long? MerchantId { get; set; }

    } 
    [DataContract]
    public class DeviceRepair
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        [Required]
        public string SerialNo { get; set; }

        [DataMember(Order = 3)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Order = 4)]
        public DateTime? FinishDate { get; set; }

        [DataMember(Order = 5)]
        public string? PicName { get; set; }
        
        [DataMember(Order = 6)]
        public string? MerchantName { get; set; }
        
        [DataMember(Order = 7)]
        public string Status { get; set; }
        [DataMember(Order = 8)]
        public string Keterangan { get; set; }
        public virtual UserProfile Engineer { set; get; }
        [ForeignKey("UserProfile")]
        public long? UserProfileId { get; set; }

        public virtual Merchant Merchant { set; get; }
        [ForeignKey("Merchant")]
        public long? MerchantId { get; set; } 
        
        public virtual Workshop Workshop { set; get; }
        [ForeignKey("Workshop")]
        public long? WorkshopId { get; set; }

    }

    [DataContract]
    public class UserProfile
    {
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [DataMember(Order = 2)]
        public string Username { get; set; }
        [DataMember(Order = 3)]
        public string Password { get; set; }
        [DataMember(Order = 4)]
        public string FullName { get; set; }
        [DataMember(Order = 5)]
        public string? Phone { get; set; }
        [DataMember(Order = 6)]
        public string? Email { get; set; }
        [DataMember(Order = 7)]
        public string? Alamat { get; set; }
        [DataMember(Order = 8)]
        public string? KTP { get; set; }
        [DataMember(Order = 9)]
        public string? PicUrl { get; set; }
        [DataMember(Order = 10)]
        public bool Aktif { get; set; } = true;

        [DataMember(Order = 11)]
        public Roles Role { set; get; } = Roles.Salesman;

        public ICollection<DeviceRequest> DeviceRequests { get; set; }
        public ICollection<DeviceRepair> DeviceRepairs { get; set; }
    }
  
    public enum Roles { Admin, HO, Salesman, Warehouse, Workshop }
    #endregion

    #region helpers
    public class DeviceExt
    {
        public Device Data { get; set; }

        public double Distance { get; set; }
    }
    
    #endregion
}