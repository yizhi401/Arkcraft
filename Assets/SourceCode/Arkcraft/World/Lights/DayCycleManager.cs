using System;
using Arkcraft.Configuration;
using Arkcraft.Utils;
using Arkcraft.Tiles;
using SourceCode.Arkcraft.Utils;
namespace Arkcraft.World.Lights
{
	public class DayCycleManager
	{
		public ArkWorld world;
		
        public Color skyColorDay = new Color(0.5f, 0.5f, 0.5f);
        public Color skyColorNight = new Color(0.0f, 0.0f, 0.0f);
        public float dayDuration;

        public Color skyColor;
        public byte ambientLightLuminance;

        private float dayTime = 0.0f;
        private DayTimeLuminanceInfo[] dayTimeLuminances;

		public DayCycleManager (ArkWorld world)
		{
			this.world = world;

            this.skyColor = skyColorDay;
            this.ambientLightLuminance = Tile.MAX_LUMINANCE;
		}
		
		public void Create(ConfigDayInfo configDayInfo)
		{
            if (configDayInfo != null)
            {
                this.dayTimeLuminances = configDayInfo.dayTimeLuminances;
                this.dayDuration = configDayInfo.dayDuration;
            }
		}
		
		public void Update(float deltaTime)
		{
            dayTime += deltaTime;

            while (dayTime > dayDuration)
                dayTime -= dayDuration;

            float normalizedDayTime = dayTime / dayDuration;

            byte newLuminance = 0;

            for (int i = 0; i < dayTimeLuminances.Length; i++)
            {
                if (dayTimeLuminances[i].toTimePercent >= normalizedDayTime)
                {
                    int targetPercent = dayTimeLuminances[i].luminancePercent;
                    float targetTime = dayTimeLuminances[i].toTimePercent;

                    int sourcePercent = dayTimeLuminances[(i - 1) % dayTimeLuminances.Length].luminancePercent;
                    float sourceTime = dayTimeLuminances[(i - 1) % dayTimeLuminances.Length].toTimePercent;

                    float normalizedDeltaTime = (normalizedDayTime - sourceTime) / (targetTime - sourceTime);

                    newLuminance = (byte)(((int)Tile.MAX_LUMINANCE) * (sourcePercent + (targetPercent - sourcePercent) * normalizedDeltaTime) / 100);

                    break;
                }
            }

            if (newLuminance != ambientLightLuminance)
            {
                ambientLightLuminance = newLuminance;

                float percentLuminance = (float) ambientLightLuminance / (float) Tile.MAX_LUMINANCE;

                skyColor = new Color(
                    skyColorNight.r + (skyColorDay.r - skyColorNight.r) * percentLuminance,
                    skyColorNight.g + (skyColorDay.g - skyColorNight.g) * percentLuminance,
                    skyColorNight.b + (skyColorDay.b - skyColorNight.b) * percentLuminance);

                world.sectorManager.UpdateAllTilesLight();
            }
		}
		
		public void Clear()
		{
            dayTimeLuminances = null;
		}

        public void Save(System.IO.BinaryWriter bw)
        {
            bw.Write(dayDuration);
            SerializationUtils.Write(bw, skyColor);
            bw.Write(ambientLightLuminance);
            bw.Write(dayTime);

            bw.Write(dayTimeLuminances.Length);
            foreach (DayTimeLuminanceInfo dayInfo in dayTimeLuminances)
            {
                bw.Write(dayInfo.luminancePercent);
                bw.Write(dayInfo.toTimePercent);
            }
        }

        public void Load(System.IO.BinaryReader br)
        {
            dayDuration = br.ReadSingle();
            skyColor = SerializationUtils.ReadColor(br);
            ambientLightLuminance = br.ReadByte();
            dayTime = br.ReadSingle();

            int n = br.ReadInt32();
            dayTimeLuminances = new DayTimeLuminanceInfo[n];
            for (int i = 0; i < n; i++)
            {
                int lp = br.ReadInt32();
                float ttp = br.ReadSingle();

                dayTimeLuminances[i] = new DayTimeLuminanceInfo(ttp, lp);
            }
        }
    }
}

