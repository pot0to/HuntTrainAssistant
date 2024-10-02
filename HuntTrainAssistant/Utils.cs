using Dalamud.Game.ClientState.Objects.Types;
using ECommons;
using ECommons.ExcelServices;
using ECommons.ExcelServices.TerritoryEnumeration;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using HuntTrainAssistant.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTrainAssistant;
public static class Utils
{
	public static bool IsNpcIdInARankList(uint npcId)
	{
			if(P.Config.Debug) return true;
			return Enum.GetValues<DawntrailARank>().Contains((DawntrailARank)npcId);
	}

	public static bool IsInHuntingTerritory()
	{
			if (ExcelTerritoryHelper.Get(Svc.ClientState.TerritoryType).TerritoryIntendedUse == (int)TerritoryIntendedUseEnum.Open_World) return true;
    if (Svc.ClientState.TerritoryType.EqualsAny((ushort[])[
        1024, //mare <-> garlemard gateway
					682, 739, 759, //doman enclave
					635, 659, //rhalgr's reach
        ])) return true; 
    if (Svc.ClientState.TerritoryType == MainCities.Idyllshire) return true;
			return false;
	}

	public static bool CanAutoInstanceSwitch()
	{
			if(P.KilledARanks.Count >= 2) return true;
			if(P.KilledARanks.Count == 1)
			{
					return Svc.Condition[ConditionFlag.InCombat] && Svc.Objects.OfType<IBattleNpc>().Any(x => Utils.IsNpcIdInARankList(x.NameId) && (float)x.CurrentHp / (float)x.MaxHp < 0.5f);
			}
			return false;
	}

    public static float ConvertMapMarkerToRawPosition(float pos, float scale = 100f)
    {
        var num = scale / 100f;
        var rawPosition = ((float)(pos - 1024.0) / num);
        return rawPosition;
    }

    public static Vector3 ConvertMapMarkerToRawPosition(FlagMapMarker flag, float scale = 100f)
    {
        return new Vector3(ConvertMapMarkerToRawPosition(flag.XFloat), 0, ConvertMapMarkerToRawPosition(flag.YFloat));
    }

    public static float ConvertMapMarkerToMapCoordinate(int pos, float scale)
    {
        var rawPosition = ConvertMapMarkerToRawPosition(pos, scale);
        return ConvertRawPositionToMapCoordinate((int)(rawPosition * 1000), scale);
    }

    public static float ConvertRawPositionToMapCoordinate(int pos, float scale)
    {
        var num = scale / 100f;
        return (float)((pos / 1000f * num + 1024.0) / 2048.0 * 41.0 / num + 1.0);
    }
}
