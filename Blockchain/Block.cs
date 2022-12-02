using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    [DataContract]
    [Serializable]
    public class Block
    {
        public int Id { get; private set; }
        public string Data { get; private set; }
        public DateTime Created { get; private set; }
        public string Hash { get; private set; }
        public string PreviousHash { get; private set; }
        public string User { get; private set; }

        public Block()
        {
            Id = 1;
            Data = "Initiate";
            Created = DateTime.Parse("14.09.2022 00:00:00.000").ToUniversalTime();
            PreviousHash = "12345";
            User = "Admin";
            Hash = GetHash(GetDataString());
        }

        public Block(string data, string user, Block previous)
        {
            if(string.IsNullOrEmpty(data) || string.IsNullOrEmpty(user)|| previous is null)
                throw new ArgumentException($"Input data incorrect.");

            Id = previous.Id + 1;
            Data = data;
            User = user;
            PreviousHash = previous.Hash;
            Created = DateTime.UtcNow;
            Hash = GetHash(GetDataString());
        }

        private string GetDataString()
        {
            var result = "";
            result = Id.ToString() 
                + Data 
                + Created.ToString("dd.MM.yyyy HH:mm:ss.fff") 
                + PreviousHash 
                + User;

            return result;
        }
        
        private string GetHash(string data)
        {
            SHA256Managed managed = new SHA256Managed();
            var hex = "";
            var hash = managed.ComputeHash(Encoding.ASCII.GetBytes(data));
            foreach (var x in hash)
                hex += String.Format("{0:x2}", x);
            return hex;
        }

        public override string ToString()
        {
            return Data;
        }

        public string SerializeToJson()
        {
            var result = "";
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                result = Encoding.UTF8.GetString(ms.ToArray());
            }

            return result;
        }

        public Block Deserialize(string json)
        {
            Block result;
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                result = (Block)jsonSerializer.ReadObject(ms);
            }

            return result;
        }
    }
}
