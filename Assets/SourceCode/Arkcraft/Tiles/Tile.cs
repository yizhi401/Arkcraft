namespace Arkcraft.Tiles
{
    /// <summary>
    ///  每一个地块
    /// </summary>
    public struct Tile
    {
        public const byte MAX_LUMINANCE = 15;
        public const byte MAX_ENERGY = 15;

        /// <summary>
        /// 地块类型
        /// </summary>
        public byte tileType;
        /// <summary>
        /// 照明
        /// </summary>
        public byte luminance;

        public byte extra;

        public byte extra2;


        //<0 means ground, >0 means artifact
        public int ArtifactId
        {
            get; set;
        }

        /// <summary>
        /// 是否在火上
        /// </summary>
        public bool OnFire
        {
            get { return (extra2 & 0x1) != 0; }
            set { if (value) extra2 |= 1; else extra2 = (byte)(extra2 & 0xFE); }
        }

        /// <summary>
        /// 是否可移动（受重力影响)
        /// </summary>
        public bool Dynamic
        {
            get { return (extra2 & 0x2) != 0; }
            set { if (value) extra2 |= 2; else extra2 = (byte)(extra2 & 0xFD); }
        }


        public byte ExtraData
        {
            get { return (byte)(extra2 >> 4); }
            set { extra2 = (byte)((extra2 & 0x0F) | (value << 4)); }
        }
		
        /// <summary>
        /// 是否投影
        /// </summary>
        public bool CastShadow
        {
            get { return (extra & 0x1) != 0; }
            set { if (value) extra |= 1; else extra = (byte) (extra & 0xFE); } 
        }

        /// <summary>
        /// 是否是光源
        /// </summary>
        public bool LightSource
        {
            get { return (extra & 0x2) != 0; }
            set { if (value) extra |= 2; else extra = (byte)(extra & 0xFD); }
        }

        /// <summary>
        /// 是否处于渲染队列
        /// </summary>
        public bool Enqueued
        {
            get { return (extra & 0x4) != 0; }
            set { if (value) extra |= 4; else extra = (byte)(extra & 0xFB); }
        }

        /// <summary>
        /// 是否已经比销毁
        /// </summary>
        public bool Destroyed
        {
            get { return (extra & 0x8) != 0; }
            set { if (value) extra |= 8; else extra = (byte)(extra & 0xF7); }
        }

        /// <summary>
        /// 含有的能量 
        /// </summary>
        public byte Energy
        {
            get { return (byte)(extra >> 4); }
            set { extra = (byte)((extra & 0x0F) | (value << 4)); }
        }

        /// <summary>
        /// 环境光照明
        /// </summary>
        public byte AmbientLuminance
        {
            get { return (byte) (luminance & 0xF); }
            set { luminance = (byte)((luminance & 0xF0) | value); }
        }

        /// <summary>
        /// 点光源照明
        /// </summary>
        public byte LightSourceLuminance
        {
            get { return (byte) (luminance >> 4); }
            set { luminance = (byte) ((luminance & 0x0F) |(value << 4)); }
        }
		
		public int Serialize()
		{
			return tileType | (luminance << 8) | (extra << 16) | (extra2 << 24);
		}
		
		public void Deserialize(uint data)
		{
			tileType = (byte) ((data >> 0) & 0xFF);
			luminance = (byte) ((data >> 8) & 0xFF);
			extra = (byte) ((data >> 16) & 0xFF);
			extra2 = (byte) ((data >> 24) & 0xFF);
		}
    }
}