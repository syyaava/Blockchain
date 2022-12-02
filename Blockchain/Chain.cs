using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blockchain
{
    public class Chain
    {
        public readonly List<Block> Blocks;
        public Block LastBlock { get; private set; }

        public Chain()
        {
            Blocks = Load();
            if(Blocks.Count == 0)
            {
                Blocks = new List<Block>();
                var genesisBlock = new Block();
                Blocks.Add(genesisBlock);
                LastBlock = Blocks.Last();
            }           
            else
            {
                if (Check())
                    LastBlock = Blocks.Last();
                else
                    throw new Exception("Ошибка получения блоков.");
            }           
        }

        public void Add(string data, string user)
        {
            var newBlock = new Block(data, user, LastBlock);
            Blocks.Add(newBlock);
            LastBlock = newBlock;
            Save();
            Sync();
        }
        private void Save()
        {
            using (var fs = new FileStream("blocks.bin", FileMode.Create))
            {
                var binFormatter = new BinaryFormatter();
                binFormatter.Serialize(fs, Blocks);
            }
        }

        public bool Check()
        {
            var genesisBlock = new Block();
            var previousHash = genesisBlock.Hash;

            foreach (var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;
                if (hash != previousHash)
                    return false;
                previousHash = block.Hash;
            }

            return true;
        }

        private List<Block> Load()
        {
            var result = new List<Block>(1000);

            using (var fs = new FileStream("blocks.bin", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    var binFormatter = new BinaryFormatter();
                    result.AddRange((List<Block>)binFormatter.Deserialize(fs));
                }
            }

            return result;
        }

        private void Sync()
        {
            //Сетевая синхронизация
        }
    }
}
