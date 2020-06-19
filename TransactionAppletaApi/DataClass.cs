using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WechatRouteApi
{
    public class DataClass : BizServiceBase<DataClass>
    {
        public List<DataModel> dataModels { get; set; }

        public class DataModel
        {
            public string NAME { get; set; }
            public List<Detail> DETAILS { get; set; }
        }

        public class Detail
        {
            /// <summary>
            /// 等级
            /// </summary>
            public int LEVEL { get; set; }
            /// <summary>
            /// 气血
            /// </summary>
            public int QX { get; set; }
            /// <summary>
            /// 闪避
            /// </summary>
            public int SB { get; set; }
            /// <summary>
            /// 攻击
            /// </summary>
            public int GJ { get; set; }
            /// <summary>
            /// 命中
            /// </summary>
            public int MZ { get; set; }
            /// <summary>
            /// 破防
            /// </summary>
            public int PF { get; set; }
            /// <summary>
            /// 抗暴
            /// </summary>
            public int KB { get; set; }
            /// <summary>
            /// 暴击
            /// </summary>
            public int BJ { get; set; }
            /// <summary>
            /// 格挡
            /// </summary>
            public int GD { get; set; }
            /// <summary>
            /// 消耗
            /// </summary>
            public int XH { get; set; }
            /// <summary>
            /// 实际战力
            /// </summary>
            public int SJZL { get; set; }
            /// <summary>
            /// 总修为
            /// </summary>
            public int ZXW { get; set; }
        }

        #region L.加载方法
        public void Initial()
        {
            var dataModelList = new List<DataModel>();

            #region 独孤九剑
            var model = new DataModel();
            model.NAME = "dgjj";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 10262, SB = 130, GJ = 352, MZ = 501, PF = 508, KB = 219, BJ = 296, GD = 292, XH = 58160, SJZL = 13255, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 11258, SB = 143, GJ = 386, MZ = 550, PF = 557, KB = 240, BJ = 325, GD = 320, XH = 87071, SJZL = 14347, ZXW = 465280 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 12705, SB = 161, GJ = 436, MZ = 621, PF = 629, KB = 271, BJ = 367, GD = 361, XH = 118184, SJZL = 15938, ZXW = 1161848 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 14597, SB = 185, GJ = 501, MZ = 713, PF = 723, KB = 311, BJ = 422, GD = 415, XH = 152154, SJZL = 18015, ZXW = 2107320 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 16927, SB = 214, GJ = 581, MZ = 827, PF = 838, KB = 361, BJ = 489, GD = 481, XH = 189602, SJZL = 20567, ZXW = 3324552 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 19687, SB = 249, GJ = 676, MZ = 962, PF = 975, KB = 420, BJ = 568, GD = 560, XH = 231112, SJZL = 23598, ZXW = 4841368 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 22871, SB = 289, GJ = 785, MZ = 1118, PF = 1133, KB = 488, BJ = 660, GD = 651, XH = 277232, SJZL = 27094, ZXW = 6690264 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 26471, SB = 334, GJ = 909, MZ = 1294, PF = 1311, KB = 565, BJ = 764, GD = 754, XH = 328472, SJZL = 31046, ZXW = 8908120 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 30482, SB = 385, GJ = 1047, MZ = 1490, PF = 1510, KB = 651, BJ = 880, GD = 868, XH = 385310, SJZL = 35452, ZXW = 11535896 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 34895, SB = 441, GJ = 1198, MZ = 1706, PF = 1728, KB = 745, BJ = 1007, GD = 994, XH = 448188, SJZL = 40291, ZXW = 14619176 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 39704, SB = 502, GJ = 1363, MZ = 1941, PF = 1966, KB = 848, BJ = 1146, GD = 1131, XH = 517515, SJZL = 45569, ZXW = 18204680 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 44901, SB = 568, GJ = 1541, MZ = 2195, PF = 2223, KB = 959, BJ = 1296, GD = 1279, XH = 593666, SJZL = 51269, ZXW = 22344800 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 50480, SB = 638, GJ = 1732, MZ = 2468, PF = 2499, KB = 1078, BJ = 1457, GD = 1438, XH = 676985, SJZL = 57386, ZXW = 27094128 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 56435, SB = 713, GJ = 1936, MZ = 2759, PF = 2794, KB = 1205, BJ = 1629, GD = 1608, XH = 767782, SJZL = 63919, ZXW = 32510008 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 62758, SB = 793, GJ = 2153, MZ = 3068, PF = 3107, KB = 1340, BJ = 1811, GD = 1788, XH = 866337, SJZL = 70854, ZXW = 38652264 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 69442, SB = 877, GJ = 2382, MZ = 3395, PF = 3438, KB = 1483, BJ = 2004, GD = 1978, XH = 972898, SJZL = 78185, ZXW = 45582960 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 76480, SB = 966, GJ = 2623, MZ = 3739, PF = 3786, KB = 1633, BJ = 2207, GD = 2178, XH = 1087684, SJZL = 85899, ZXW = 53366144 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 83865, SB = 1059, GJ = 2876, MZ = 4100, PF = 4152, KB = 1791, BJ = 2420, GD = 2388, XH = 1210884, SJZL = 93998, ZXW = 62067616 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 91591, SB = 1157, GJ = 3141, MZ = 4478, PF = 4535, KB = 1956, BJ = 2643, GD = 2608, XH = 1342657, SJZL = 102479, ZXW = 71754688 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 99650, SB = 1259, GJ = 3417, MZ = 4872, PF = 4934, KB = 2128, BJ = 2875, GD = 2837, XH = 1483134, SJZL = 111314, ZXW = 82495944 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 108036, SB = 1365, GJ = 3705, MZ = 5282, PF = 5349, KB = 2307, BJ = 3117, GD = 3076, XH = 1632420, SJZL = 120516, ZXW = 94361016 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 116741, SB = 1475, GJ = 4004, MZ = 5707, PF = 5780, KB = 2493, BJ = 3368, GD = 3324, XH = 1790590, SJZL = 130066, ZXW = 107419576 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 125758, SB = 1589, GJ = 4313, MZ = 6148, PF = 6226, KB = 2686, BJ = 3628, GD = 3581, XH = 1957693, SJZL = 139958, ZXW = 121744296 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 135081, SB = 1707, GJ = 4633, MZ = 6604, PF = 6688, KB = 2885, BJ = 3897, GD = 3847, XH = 2133753, SJZL = 150191, ZXW = 137405840 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 144703, SB = 1828, GJ = 4963, MZ = 7074, PF = 7164, KB = 3091, BJ = 4174, GD = 4121, XH = 2318768, SJZL = 160741, ZXW = 154475864 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 154616, SB = 1953, GJ = 5303, MZ = 7558, PF = 7655, KB = 3303, BJ = 4460, GD = 4403, XH = 2512711, SJZL = 171613, ZXW = 173026008 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 164814, SB = 2082, GJ = 5653, MZ = 8056, PF = 8160, KB = 3521, BJ = 4754, GD = 4693, XH = 2715529, SJZL = 182799, ZXW = 193127696 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 175290, SB = 2214, GJ = 6012, MZ = 8568, PF = 8679, KB = 3745, BJ = 5056, GD = 4991, XH = 2927149, SJZL = 194288, ZXW = 214851928 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 186037, SB = 2350, GJ = 6381, MZ = 9093, PF = 9211, KB = 3975, BJ = 5366, GD = 5297, XH = 3147471, SJZL = 206080, ZXW = 238269120 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 197048, SB = 2489, GJ = 6759, MZ = 9631, PF = 9756, KB = 4210, BJ = 5683, GD = 5611, XH = 3376375, SJZL = 218157, ZXW = 263448888 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 吸星大法
            model = new DataModel();
            model.NAME = "xxdf";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 10161, SB = 387, GJ = 384, MZ = 252, PF = 200, KB = 445, BJ = 519, GD = 255, XH = 58160, SJZL = 13846, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 11098, SB = 423, GJ = 419, MZ = 275, PF = 218, KB = 486, BJ = 567, GD = 279, XH = 87071, SJZL = 14938, ZXW = 465280 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 12460, SB = 475, GJ = 470, MZ = 309, PF = 245, KB = 546, BJ = 637, GD = 313, XH = 118184, SJZL = 16528, ZXW = 1161848 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 14240, SB = 543, GJ = 537, MZ = 353, PF = 280, KB = 624, BJ = 728, GD = 358, XH = 152154, SJZL = 18603, ZXW = 2107320 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 16432, SB = 627, GJ = 620, MZ = 407, PF = 323, KB = 720, BJ = 840, GD = 413, XH = 189602, SJZL = 21160, ZXW = 3324552 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 19029, SB = 726, GJ = 718, MZ = 471, PF = 374, KB = 834, BJ = 973, GD = 478, XH = 231112, SJZL = 24186, ZXW = 4841368 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 22025, SB = 840, GJ = 831, MZ = 545, PF = 433, KB = 965, BJ = 1126, GD = 553, XH = 277232, SJZL = 27675, ZXW = 6690264 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 25413, SB = 969, GJ = 959, MZ = 629, PF = 500, KB = 1113, BJ = 1299, GD = 638, XH = 328472, SJZL = 31623, ZXW = 8908120 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 29186, SB = 1113, GJ = 1102, MZ = 722, PF = 574, KB = 1278, BJ = 1492, GD = 733, XH = 385310, SJZL = 36023, ZXW = 11535896 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 33338, SB = 1271, GJ = 1259, MZ = 825, PF = 656, KB = 1460, BJ = 1704, GD = 837, XH = 448188, SJZL = 40864, ZXW = 14619176 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 37863, SB = 1443, GJ = 1430, MZ = 937, PF = 745, KB = 1658, BJ = 1935, GD = 950, XH = 517515, SJZL = 46133, ZXW = 18204680 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 42753, SB = 1629, GJ = 1615, MZ = 1058, PF = 841, KB = 1872, BJ = 2185, GD = 1073, XH = 593666, SJZL = 51833, ZXW = 22344800 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 48002, SB = 1829, GJ = 1813, MZ = 1188, PF = 944, KB = 2102, BJ = 2453, GD = 1205, XH = 676985, SJZL = 57950, ZXW = 27094128 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 53605, SB = 2042, GJ = 2025, MZ = 1327, PF = 1054, KB = 2347, BJ = 2739, GD = 1346, XH = 767782, SJZL = 64480, ZXW = 32510008 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 59554, SB = 2269, GJ = 2250, MZ = 1474, PF = 1171, KB = 2608, BJ = 3043, GD = 1495, XH = 866337, SJZL = 71417, ZXW = 38652264 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 65843, SB = 2509, GJ = 2488, MZ = 1630, PF = 1295, KB = 2884, BJ = 3364, GD = 1653, XH = 972898, SJZL = 78755, ZXW = 45582960 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 72465, SB = 2761, GJ = 2738, MZ = 1794, PF = 1425, KB = 3174, BJ = 3702, GD = 1819, XH = 1087684, SJZL = 86468, ZXW = 53366144 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 79413, SB = 3026, GJ = 3000, MZ = 1966, PF = 1562, KB = 3478, BJ = 4057, GD = 1993, XH = 1210884, SJZL = 94564, ZXW = 62067616 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 86682, SB = 3303, GJ = 3275, MZ = 2146, PF = 1705, KB = 3796, BJ = 4429, GD = 2175, XH = 1342657, SJZL = 103039, ZXW = 71754688 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 94264, SB = 3592, GJ = 3561, MZ = 2334, PF = 1854, KB = 4128, BJ = 4817, GD = 2365, XH = 1483134, SJZL = 111877, ZXW = 82495944 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 102154, SB = 3893, GJ = 3859, MZ = 2529, PF = 2009, KB = 4474, BJ = 5220, GD = 2563, XH = 1632420, SJZL = 121074, ZXW = 94361016 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 110344, SB = 4205, GJ = 4168, MZ = 2732, PF = 2170, KB = 4833, BJ = 5639, GD = 2768, XH = 1790590, SJZL = 130620, ZXW = 107419576 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 118828, SB = 4528, GJ = 4489, MZ = 2942, PF = 2337, KB = 5205, BJ = 6073, GD = 2981, XH = 1957693, SJZL = 140515, ZXW = 121744296 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 127600, SB = 4862, GJ = 4820, MZ = 3159, PF = 2510, KB = 5589, BJ = 6521, GD = 3201, XH = 2133753, SJZL = 150736, ZXW = 137405840 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 136653, SB = 5207, GJ = 5162, MZ = 3383, PF = 2688, KB = 5986, BJ = 6984, GD = 3428, XH = 2318768, SJZL = 161290, ZXW = 154475864 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 145980, SB = 5562, GJ = 5514, MZ = 3614, PF = 2872, KB = 6395, BJ = 7461, GD = 3662, XH = 2512711, SJZL = 172165, ZXW = 173026008 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 155575, SB = 5928, GJ = 5876, MZ = 3852, PF = 3061, KB = 6815, BJ = 7951, GD = 3903, XH = 2715529, SJZL = 183350, ZXW = 193127696 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 165431, SB = 6303, GJ = 6248, MZ = 4096, PF = 3255, KB = 7247, BJ = 8455, GD = 4150, XH = 2927149, SJZL = 194837, ZXW = 214851928 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 175542, SB = 6688, GJ = 6630, MZ = 4346, PF = 3454, KB = 7690, BJ = 8972, GD = 4404, XH = 3147471, SJZL = 206624, ZXW = 238269120 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 185902, SB = 7083, GJ = 7021, MZ = 4603, PF = 3658, KB = 8144, BJ = 9502, GD = 4664, XH = 3376375, SJZL = 218706, ZXW = 263448888 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 易筋经
            model = new DataModel();
            model.NAME = "yjj";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 12769, SB = 271, GJ = 404, MZ = 390, PF = 249, KB = 325, BJ = 292, GD = 505, XH = 58160, SJZL = 14277, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 13907, SB = 295, GJ = 440, MZ = 425, PF = 271, KB = 354, BJ = 318, GD = 550, XH = 87071, SJZL = 15371, ZXW = 465280 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 15560, SB = 330, GJ = 492, MZ = 475, PF = 303, KB = 396, BJ = 356, GD = 615, XH = 118184, SJZL = 16954, ZXW = 1161848 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 17721, SB = 376, GJ = 560, MZ = 541, PF = 345, KB = 451, BJ = 405, GD = 700, XH = 152154, SJZL = 19026, ZXW = 2107320 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 20381, SB = 432, GJ = 644, MZ = 622, PF = 397, KB = 519, BJ = 466, GD = 805, XH = 189602, SJZL = 21582, ZXW = 3324552 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 23533, SB = 499, GJ = 744, MZ = 718, PF = 458, KB = 599, BJ = 538, GD = 930, XH = 231112, SJZL = 24611, ZXW = 4841368 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 27169, SB = 576, GJ = 859, MZ = 829, PF = 529, KB = 691, BJ = 621, GD = 1074, XH = 277232, SJZL = 28104, ZXW = 6690264 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 31280, SB = 663, GJ = 989, MZ = 954, PF = 609, KB = 796, BJ = 715, GD = 1237, XH = 328472, SJZL = 32055, ZXW = 8908120 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 35860, SB = 760, GJ = 1134, MZ = 1094, PF = 698, KB = 912, BJ = 820, GD = 1418, XH = 385310, SJZL = 36456, ZXW = 11535896 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 40899, SB = 867, GJ = 1293, MZ = 1248, PF = 796, KB = 1040, BJ = 935, GD = 1617, XH = 448188, SJZL = 41294, ZXW = 14619176 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 46391, SB = 983, GJ = 1467, MZ = 1416, PF = 903, KB = 1180, BJ = 1061, GD = 1834, XH = 517515, SJZL = 46576, ZXW = 18204680 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 52326, SB = 1109, GJ = 1655, MZ = 1597, PF = 1019, KB = 1331, BJ = 1197, GD = 2069, XH = 593666, SJZL = 52285, ZXW = 22344800 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 58697, SB = 1244, GJ = 1857, MZ = 1791, PF = 1143, KB = 1493, BJ = 1343, GD = 2321, XH = 676985, SJZL = 58409, ZXW = 27094128 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 65497, SB = 1388, GJ = 2072, MZ = 1998, PF = 1276, KB = 1666, BJ = 1499, GD = 2590, XH = 767782, SJZL = 64945, ZXW = 32510008 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 72718, SB = 1541, GJ = 2300, MZ = 2218, PF = 1417, KB = 1850, BJ = 1664, GD = 2876, XH = 866337, SJZL = 71885, ZXW = 38652264 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 80351, SB = 1703, GJ = 2541, MZ = 2451, PF = 1566, KB = 2044, BJ = 1839, GD = 3178, XH = 972898, SJZL = 79222, ZXW = 45582960 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 88388, SB = 1873, GJ = 2795, MZ = 2696, PF = 1723, KB = 2248, BJ = 2023, GD = 3496, XH = 1087684, SJZL = 86943, ZXW = 53366144 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 96821, SB = 2052, GJ = 3062, MZ = 2953, PF = 1887, KB = 2462, BJ = 2216, GD = 3830, XH = 1210884, SJZL = 95048, ZXW = 62067616 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 105643, SB = 2239, GJ = 3341, MZ = 3222, PF = 2059, KB = 2686, BJ = 2418, GD = 4179, XH = 1342657, SJZL = 103525, ZXW = 71754688 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 114845, SB = 2434, GJ = 3632, MZ = 3503, PF = 2238, KB = 2920, BJ = 2629, GD = 4543, XH = 1483134, SJZL = 112370, ZXW = 82495944 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 124421, SB = 2637, GJ = 3935, MZ = 3795, PF = 2425, KB = 3163, BJ = 2848, GD = 4922, XH = 1632420, SJZL = 121573, ZXW = 94361016 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 134361, SB = 2848, GJ = 4249, MZ = 4098, PF = 2619, KB = 3416, BJ = 3075, GD = 5315, XH = 1790590, SJZL = 131123, ZXW = 107419576 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 144658, SB = 3066, GJ = 4575, MZ = 4412, PF = 2820, KB = 3678, BJ = 3311, GD = 5722, XH = 1957693, SJZL = 141022, ZXW = 121744296 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 155304, SB = 3292, GJ = 4912, MZ = 4737, PF = 3028, KB = 3949, BJ = 3555, GD = 6143, XH = 2133753, SJZL = 151261, ZXW = 137405840 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 166291, SB = 3525, GJ = 5260, MZ = 5072, PF = 3242, KB = 4228, BJ = 3806, GD = 6578, XH = 2318768, SJZL = 161820, ZXW = 154475864 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 177612, SB = 3765, GJ = 5618, MZ = 5417, PF = 3463, KB = 4516, BJ = 4065, GD = 7026, XH = 2512711, SJZL = 172701, ZXW = 173026008 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 189257, SB = 4012, GJ = 5986, MZ = 5772, PF = 3690, KB = 4812, BJ = 4331, GD = 7487, XH = 2715529, SJZL = 183890, ZXW = 193127696 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 201220, SB = 4265, GJ = 6364, MZ = 6137, PF = 3923, KB = 5116, BJ = 4605, GD = 7960, XH = 2927149, SJZL = 195382, ZXW = 214851928 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 213492, SB = 4525, GJ = 6752, MZ = 6511, PF = 4162, KB = 5428, BJ = 4886, GD = 8445, XH = 3147471, SJZL = 207171, ZXW = 238269120 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 226066, SB = 4791, GJ = 7150, MZ = 6895, PF = 4407, KB = 5748, BJ = 5174, GD = 8942, XH = 3376375, SJZL = 219257, ZXW = 263448888 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 化功大法
            model = new DataModel();
            model.NAME = "hgdf";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 5491, SB = 231, GJ = 202, MZ = 164, PF = 250, KB = 145, BJ = 212, GD = 154, XH = 37067, SJZL = 7550, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 6223, SB = 262, GJ = 229, MZ = 186, PF = 283, KB = 164, BJ = 240, GD = 175, XH = 55171, SJZL = 8423, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 7287, SB = 307, GJ = 268, MZ = 218, PF = 332, KB = 192, BJ = 281, GD = 205, XH = 74351, SJZL = 9695, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 8678, SB = 366, GJ = 319, MZ = 259, PF = 395, KB = 229, BJ = 335, GD = 244, XH = 94944, SJZL = 11354, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 10391, SB = 438, GJ = 382, MZ = 310, PF = 473, KB = 274, BJ = 401, GD = 292, XH = 117267, SJZL = 13395, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 12421, SB = 523, GJ = 457, MZ = 371, PF = 566, KB = 327, BJ = 479, GD = 349, XH = 141618, SJZL = 15816, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 14762, SB = 622, GJ = 543, MZ = 441, PF = 673, KB = 389, BJ = 569, GD = 415, XH = 168279, SJZL = 18612, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 17410, SB = 733, GJ = 641, MZ = 520, PF = 794, KB = 459, BJ = 671, GD = 489, XH = 197511, SJZL = 21772, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 20360, SB = 857, GJ = 750, MZ = 608, PF = 929, KB = 537, BJ = 785, GD = 572, XH = 229558, SJZL = 25296, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 23606, SB = 994, GJ = 870, MZ = 705, PF = 1077, KB = 622, BJ = 910, GD = 663, XH = 264647, SJZL = 29169, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 27142, SB = 1143, GJ = 1000, MZ = 810, PF = 1238, KB = 715, BJ = 1046, GD = 762, XH = 302986, SJZL = 33381, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 30965, SB = 1304, GJ = 1141, MZ = 924, PF = 1412, KB = 816, BJ = 1194, GD = 869, XH = 344770, SJZL = 37943, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 35069, SB = 1477, GJ = 1292, MZ = 1046, PF = 1599, KB = 924, BJ = 1352, GD = 984, XH = 390172, SJZL = 42834, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 39448, SB = 1661, GJ = 1453, MZ = 1177, PF = 1799, KB = 1039, BJ = 1521, GD = 1107, XH = 439352, SJZL = 48057, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 44098, SB = 1857, GJ = 1624, MZ = 1316, PF = 2011, KB = 1161, BJ = 1700, GD = 1238, XH = 492453, SJZL = 53603, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 49014, SB = 2064, GJ = 1805, MZ = 1463, PF = 2235, KB = 1290, BJ = 1890, GD = 1376, XH = 549603, SJZL = 59467, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 54190, SB = 2282, GJ = 1996, MZ = 1617, PF = 2471, KB = 1426, BJ = 2090, GD = 1521, XH = 610913, SJZL = 65641, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 59622, SB = 2511, GJ = 2196, MZ = 1779, PF = 2719, KB = 1569, BJ = 2300, GD = 1673, XH = 676480, SJZL = 72122, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 65304, SB = 2750, GJ = 2405, MZ = 1948, PF = 2978, KB = 1719, BJ = 2519, GD = 1832, XH = 746386, SJZL = 78894, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 71231, SB = 3000, GJ = 2623, MZ = 2125, PF = 3248, KB = 1875, BJ = 2748, GD = 1998, XH = 820698, SJZL = 85964, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 77399, SB = 3260, GJ = 2850, MZ = 2309, PF = 3529, KB = 2037, BJ = 2986, GD = 2171, XH = 899469, SJZL = 93319, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 83801, SB = 3530, GJ = 3086, MZ = 2500, PF = 3821, KB = 2205, BJ = 3233, GD = 2351, XH = 982737, SJZL = 100958, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 90433, SB = 3809, GJ = 3330, MZ = 2698, PF = 4124, KB = 2380, BJ = 3489, GD = 2537, XH = 1070529, SJZL = 108871, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 97289, SB = 4098, GJ = 3583, MZ = 2902, PF = 4437, KB = 2560, BJ = 3754, GD = 2729, XH = 1162856, SJZL = 117051, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 104365, SB = 4396, GJ = 3844, MZ = 3113, PF = 4760, KB = 2746, BJ = 4027, GD = 2928, XH = 1259717, SJZL = 125495, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 111656, SB = 4703, GJ = 4113, MZ = 3330, PF = 5093, KB = 2938, BJ = 4308, GD = 3133, XH = 1361098, SJZL = 134195, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 119156, SB = 5019, GJ = 4389, MZ = 3554, PF = 5435, KB = 3135, BJ = 4597, GD = 3344, XH = 1466973, SJZL = 143141, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 126860, SB = 5343, GJ = 4673, MZ = 3784, PF = 5786, KB = 3338, BJ = 4894, GD = 3560, XH = 1577303, SJZL = 152329, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 134764, SB = 5676, GJ = 4964, MZ = 4020, PF = 6147, KB = 3546, BJ = 5199, GD = 3782, XH = 1692038, SJZL = 161762, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 142862, SB = 6017, GJ = 5262, MZ = 4262, PF = 6516, KB = 3759, BJ = 5512, GD = 4009, XH = 1811117, SJZL = 171421, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 清心普善咒
            model = new DataModel();
            model.NAME = "qxpsz";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 7576, SB = 161, GJ = 182, MZ = 108, PF = 295, KB = 250, BJ = 82, GD = 187, XH = 37067, SJZL = 7506, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 8593, SB = 183, GJ = 206, MZ = 122, PF = 335, KB = 284, BJ = 93, GD = 212, XH = 55171, SJZL = 8379, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 10072, SB = 214, GJ = 242, MZ = 143, PF = 392, KB = 333, BJ = 109, GD = 249, XH = 74351, SJZL = 9650, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 12005, SB = 255, GJ = 289, MZ = 170, PF = 467, KB = 397, BJ = 130, GD = 297, XH = 94944, SJZL = 11311, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 14385, SB = 306, GJ = 346, MZ = 204, PF = 560, KB = 476, BJ = 156, GD = 356, XH = 117267, SJZL = 13360, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 17205, SB = 366, GJ = 414, MZ = 244, PF = 670, KB = 569, BJ = 186, GD = 426, XH = 141618, SJZL = 15782, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 20458, SB = 435, GJ = 492, MZ = 290, PF = 796, KB = 676, BJ = 221, GD = 506, XH = 168279, SJZL = 18567, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 24137, SB = 513, GJ = 581, MZ = 342, PF = 939, KB = 797, BJ = 261, GD = 597, XH = 197511, SJZL = 21725, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 28235, SB = 600, GJ = 680, MZ = 400, PF = 1098, KB = 932, BJ = 305, GD = 698, XH = 229558, SJZL = 25239, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 32744, SB = 696, GJ = 789, MZ = 464, PF = 1273, KB = 1081, BJ = 354, GD = 809, XH = 264647, SJZL = 29111, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 37657, SB = 801, GJ = 907, MZ = 534, PF = 1464, KB = 1243, BJ = 407, GD = 930, XH = 302986, SJZL = 33328, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 42968, SB = 914, GJ = 1035, MZ = 609, PF = 1671, KB = 1418, BJ = 464, GD = 1061, XH = 344770, SJZL = 37885, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 48669, SB = 1035, GJ = 1172, MZ = 690, PF = 1893, KB = 1606, BJ = 526, GD = 1202, XH = 390172, SJZL = 42780, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 54753, SB = 1165, GJ = 1318, MZ = 776, PF = 2130, KB = 1807, BJ = 592, GD = 1352, XH = 439352, SJZL = 48004, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 61213, SB = 1303, GJ = 1474, MZ = 868, PF = 2381, KB = 2020, BJ = 662, GD = 1512, XH = 492453, SJZL = 53557, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 68042, SB = 1448, GJ = 1638, MZ = 965, PF = 2647, KB = 2246, BJ = 736, GD = 1681, XH = 549603, SJZL = 59423, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 75233, SB = 1601, GJ = 1811, MZ = 1067, PF = 2927, KB = 2483, BJ = 814, GD = 1859, XH = 610913, SJZL = 65599, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 82779, SB = 1762, GJ = 1993, MZ = 1174, PF = 3220, KB = 2732, BJ = 896, GD = 2045, XH = 676480, SJZL = 72078, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 90672, SB = 1930, GJ = 2183, MZ = 1286, PF = 3527, KB = 2993, BJ = 981, GD = 2240, XH = 746386, SJZL = 78856, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 98906, SB = 2105, GJ = 2381, MZ = 1403, PF = 3847, KB = 3265, BJ = 1070, GD = 2443, XH = 820698, SJZL = 85922, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 107474, SB = 2287, GJ = 2587, MZ = 1525, PF = 4180, KB = 3548, BJ = 1163, GD = 2655, XH = 899469, SJZL = 93280, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 116368, SB = 2476, GJ = 2801, MZ = 1651, PF = 4526, KB = 3842, BJ = 1259, GD = 2875, XH = 982737, SJZL = 100917, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 125581, SB = 2672, GJ = 3023, MZ = 1782, PF = 4884, KB = 4146, BJ = 1359, GD = 3102, XH = 1070529, SJZL = 108826, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 135106, SB = 2875, GJ = 3252, MZ = 1917, PF = 5254, KB = 4461, BJ = 1462, GD = 3337, XH = 1162856, SJZL = 117003, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 144936, SB = 3084, GJ = 3489, MZ = 2057, PF = 5636, KB = 4786, BJ = 1568, GD = 3580, XH = 1259717, SJZL = 125446, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 155064, SB = 3300, GJ = 3733, MZ = 2201, PF = 6030, KB = 5120, BJ = 1677, GD = 3830, XH = 1361098, SJZL = 134142, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 165483, SB = 3522, GJ = 3984, MZ = 2349, PF = 6435, KB = 5464, BJ = 1790, GD = 4087, XH = 1466973, SJZL = 143089, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 176186, SB = 3750, GJ = 4242, MZ = 2501, PF = 6851, KB = 5817, BJ = 1906, GD = 4351, XH = 1577303, SJZL = 152279, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 187167, SB = 3984, GJ = 4506, MZ = 2657, PF = 7278, KB = 6180, BJ = 2025, GD = 4622, XH = 1692038, SJZL = 161709, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 198418, SB = 4224, GJ = 4777, MZ = 2817, PF = 7716, KB = 6552, BJ = 2147, GD = 4900, XH = 1811117, SJZL = 171380, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 生死符
            model = new DataModel();
            model.NAME = "ssf";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 4991, SB = 106, GJ = 170, MZ = 201, PF = 246, KB = 198, BJ = 230, GD = 219, XH = 37067, SJZL = 7483, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 5664, SB = 120, GJ = 193, MZ = 228, PF = 279, KB = 225, BJ = 261, GD = 248, XH = 55171, SJZL = 8354, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 6642, SB = 141, GJ = 226, MZ = 267, PF = 327, KB = 264, BJ = 306, GD = 291, XH = 74351, SJZL = 9622, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 7921, SB = 168, GJ = 269, MZ = 319, PF = 390, KB = 315, BJ = 365, GD = 347, XH = 94944, SJZL = 11282, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 9496, SB = 201, GJ = 322, MZ = 382, PF = 467, KB = 377, BJ = 437, GD = 416, XH = 117267, SJZL = 13314, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 11362, SB = 241, GJ = 385, MZ = 457, PF = 559, KB = 451, BJ = 523, GD = 498, XH = 141618, SJZL = 15736, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 13514, SB = 287, GJ = 458, MZ = 544, PF = 665, KB = 536, BJ = 622, GD = 592, XH = 168279, SJZL = 18527, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 15948, SB = 339, GJ = 541, MZ = 642, PF = 785, KB = 633, BJ = 734, GD = 699, XH = 197511, SJZL = 21692, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 18660, SB = 397, GJ = 633, MZ = 751, PF = 918, KB = 741, BJ = 859, GD = 818, XH = 229558, SJZL = 25212, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 21644, SB = 460, GJ = 734, MZ = 871, PF = 1065, KB = 859, BJ = 996, GD = 949, XH = 264647, SJZL = 29078, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 24895, SB = 529, GJ = 844, MZ = 1002, PF = 1225, KB = 988, BJ = 1146, GD = 1091, XH = 302986, SJZL = 33294, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 28409, SB = 604, GJ = 963, MZ = 1144, PF = 1398, KB = 1127, BJ = 1308, GD = 1245, XH = 344770, SJZL = 37855, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 32181, SB = 684, GJ = 1091, MZ = 1296, PF = 1584, KB = 1277, BJ = 1482, GD = 1410, XH = 390172, SJZL = 42751, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 36207, SB = 770, GJ = 1228, MZ = 1458, PF = 1782, KB = 1437, BJ = 1667, GD = 1586, XH = 439352, SJZL = 47975, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 40482, SB = 861, GJ = 1373, MZ = 1630, PF = 1992, KB = 1607, BJ = 1864, GD = 1773, XH = 492453, SJZL = 53521, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 45001, SB = 957, GJ = 1526, MZ = 1812, PF = 2214, KB = 1786, BJ = 2072, GD = 1971, XH = 549603, SJZL = 59379, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 49759, SB = 1058, GJ = 1688, MZ = 2004, PF = 2448, KB = 1975, BJ = 2291, GD = 2179, XH = 610913, SJZL = 65554, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 54752, SB = 1164, GJ = 1858, MZ = 2205, PF = 2694, KB = 2173, BJ = 2521, GD = 2398, XH = 676480, SJZL = 72036, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 59975, SB = 1275, GJ = 2035, MZ = 2416, PF = 2951, KB = 2380, BJ = 2761, GD = 2627, XH = 746386, SJZL = 78812, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 65424, SB = 1391, GJ = 2220, MZ = 2636, PF = 3219, KB = 2596, BJ = 3012, GD = 2866, XH = 820698, SJZL = 85885, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 71094, SB = 1512, GJ = 2413, MZ = 2865, PF = 3498, KB = 2821, BJ = 3273, GD = 3114, XH = 899469, SJZL = 93247, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 76979, SB = 1637, GJ = 2613, MZ = 3102, PF = 3788, KB = 3055, BJ = 3544, GD = 3372, XH = 982737, SJZL = 100888, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 83076, SB = 1767, GJ = 2820, MZ = 3348, PF = 4088, KB = 3297, BJ = 3825, GD = 3639, XH = 1070529, SJZL = 108803, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 89379, SB = 1901, GJ = 3034, MZ = 3602, PF = 4398, KB = 3547, BJ = 4115, GD = 3915, XH = 1162856, SJZL = 116979, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 95884, SB = 2039, GJ = 3255, MZ = 3864, PF = 4718, KB = 3805, BJ = 4414, GD = 4200, XH = 1259717, SJZL = 125417, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 102586, SB = 2182, GJ = 3483, MZ = 4134, PF = 5048, KB = 4071, BJ = 4723, GD = 4494, XH = 1361098, SJZL = 134122, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 109481, SB = 2329, GJ = 3717, MZ = 4412, PF = 5387, KB = 4345, BJ = 5040, GD = 4796, XH = 1466973, SJZL = 143069, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 116564, SB = 2480, GJ = 3958, MZ = 4698, PF = 5736, KB = 4626, BJ = 5366, GD = 5106, XH = 1577303, SJZL = 152266, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 123830, SB = 2635, GJ = 4205, MZ = 4991, PF = 6094, KB = 4914, BJ = 5700, GD = 5424, XH = 1692038, SJZL = 161696, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 131275, SB = 2793, GJ = 4458, MZ = 5291, PF = 6460, KB = 5209, BJ = 6043, GD = 5750, XH = 1811117, SJZL = 171354, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 圣火令
            model = new DataModel();
            model.NAME = "shl";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 5092, SB = 89, GJ = 186, MZ = 224, PF = 202, KB = 226, BJ = 249, GD = 203, XH = 37067, SJZL = 7552, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 5771, SB = 101, GJ = 211, MZ = 254, PF = 229, KB = 256, BJ = 282, GD = 230, XH = 55171, SJZL = 8426, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 6758, SB = 118, GJ = 247, MZ = 297, PF = 268, KB = 300, BJ = 330, GD = 269, XH = 74351, SJZL = 9690, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 8048, SB = 141, GJ = 294, MZ = 354, PF = 319, KB = 357, BJ = 393, GD = 320, XH = 94944, SJZL = 11348, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 9636, SB = 169, GJ = 352, MZ = 424, PF = 382, KB = 427, BJ = 471, GD = 383, XH = 117267, SJZL = 13391, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 11518, SB = 202, GJ = 421, MZ = 507, PF = 456, KB = 510, BJ = 563, GD = 458, XH = 141618, SJZL = 15810, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 13689, SB = 240, GJ = 500, MZ = 603, PF = 542, KB = 606, BJ = 669, GD = 545, XH = 168279, SJZL = 18602, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 16144, SB = 283, GJ = 590, MZ = 711, PF = 639, KB = 715, BJ = 789, GD = 643, XH = 197511, SJZL = 21762, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 18879, SB = 331, GJ = 690, MZ = 831, PF = 747, KB = 836, BJ = 923, GD = 752, XH = 229558, SJZL = 25277, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 21888, SB = 384, GJ = 800, MZ = 964, PF = 866, KB = 969, BJ = 1070, GD = 872, XH = 264647, SJZL = 29149, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 25167, SB = 442, GJ = 920, MZ = 1108, PF = 996, KB = 1114, BJ = 1230, GD = 1003, XH = 302986, SJZL = 33367, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 28712, SB = 504, GJ = 1050, MZ = 1264, PF = 1136, KB = 1271, BJ = 1403, GD = 1144, XH = 344770, SJZL = 37924, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 32517, SB = 571, GJ = 1189, MZ = 1432, PF = 1287, KB = 1440, BJ = 1589, GD = 1296, XH = 390172, SJZL = 42825, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 36578, SB = 642, GJ = 1338, MZ = 1611, PF = 1448, KB = 1620, BJ = 1787, GD = 1458, XH = 439352, SJZL = 48051, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 40890, SB = 718, GJ = 1496, MZ = 1801, PF = 1619, KB = 1811, BJ = 1997, GD = 1630, XH = 492453, SJZL = 53600, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 45448, SB = 798, GJ = 1663, MZ = 2002, PF = 1799, KB = 2013, BJ = 2219, GD = 1812, XH = 549603, SJZL = 59463, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 50248, SB = 882, GJ = 1839, MZ = 2213, PF = 1989, KB = 2226, BJ = 2453, GD = 2003, XH = 610913, SJZL = 65635, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 55285, SB = 970, GJ = 2023, MZ = 2435, PF = 2188, KB = 2449, BJ = 2699, GD = 2204, XH = 676480, SJZL = 72110, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 60553, SB = 1063, GJ = 2216, MZ = 2667, PF = 2397, KB = 2683, BJ = 2956, GD = 2414, XH = 746386, SJZL = 78893, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 66049, SB = 1160, GJ = 2417, MZ = 2909, PF = 2615, KB = 2927, BJ = 3224, GD = 2633, XH = 820698, SJZL = 85966, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 71768, SB = 1260, GJ = 2626, MZ = 3161, PF = 2841, KB = 3181, BJ = 3503, GD = 2861, XH = 899469, SJZL = 93320, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 77705, SB = 1364, GJ = 2843, MZ = 3422, PF = 3076, KB = 3444, BJ = 3793, GD = 3098, XH = 982737, SJZL = 100955, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 83854, SB = 1472, GJ = 3068, MZ = 3693, PF = 3319, KB = 3717, BJ = 4093, GD = 3343, XH = 1070529, SJZL = 108865, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 90212, SB = 1584, GJ = 3301, MZ = 3973, PF = 3571, KB = 3999, BJ = 4403, GD = 3597, XH = 1162856, SJZL = 117049, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 96773, SB = 1699, GJ = 3541, MZ = 4262, PF = 3831, KB = 4290, BJ = 4723, GD = 3859, XH = 1259717, SJZL = 125491, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 103533, SB = 1818, GJ = 3788, MZ = 4560, PF = 4099, KB = 4590, BJ = 5053, GD = 4129, XH = 1361098, SJZL = 134192, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 110487, SB = 1940, GJ = 4043, MZ = 4866, PF = 4374, KB = 4898, BJ = 5392, GD = 4406, XH = 1466973, SJZL = 143134, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 117631, SB = 2065, GJ = 4305, MZ = 5181, PF = 4657, KB = 5215, BJ = 5741, GD = 4691, XH = 1577303, SJZL = 152331, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 124960, SB = 2194, GJ = 4573, MZ = 5504, PF = 4947, KB = 5540, BJ = 6099, GD = 4983, XH = 1692038, SJZL = 161761, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 132469, SB = 2326, GJ = 4848, MZ = 5835, PF = 5244, KB = 5873, BJ = 6466, GD = 5282, XH = 1811117, SJZL = 171423, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 紫霞神功
            model = new DataModel();
            model.NAME = "zxsg";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 6358, SB = 212, GJ = 173, MZ = 173, PF = 173, KB = 212, BJ = 173, GD = 212, XH = 37067, SJZL = 7546, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 7206, SB = 240, GJ = 196, MZ = 196, PF = 196, KB = 240, BJ = 196, GD = 240, XH = 55171, SJZL = 8414, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 8438, SB = 281, GJ = 230, MZ = 230, PF = 230, KB = 281, BJ = 230, GD = 281, XH = 74351, SJZL = 9689, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 10049, SB = 335, GJ = 274, MZ = 274, PF = 274, KB = 335, BJ = 274, GD = 335, XH = 94944, SJZL = 11354, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 12032, SB = 401, GJ = 328, MZ = 328, PF = 328, KB = 401, BJ = 328, GD = 401, XH = 117267, SJZL = 13394, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 14382, SB = 479, GJ = 392, MZ = 392, PF = 392, KB = 479, BJ = 392, GD = 479, XH = 141618, SJZL = 15810, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 17093, SB = 569, GJ = 466, MZ = 466, PF = 466, KB = 569, BJ = 466, GD = 569, XH = 168279, SJZL = 18600, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 20159, SB = 671, GJ = 550, MZ = 550, PF = 550, KB = 671, BJ = 550, GD = 671, XH = 197511, SJZL = 21763, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 23574, SB = 785, GJ = 643, MZ = 643, PF = 643, KB = 785, BJ = 643, GD = 785, XH = 229558, SJZL = 25281, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 27332, SB = 910, GJ = 745, MZ = 745, PF = 745, KB = 910, BJ = 745, GD = 910, XH = 264647, SJZL = 29141, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 31427, SB = 1046, GJ = 857, MZ = 857, PF = 857, KB = 1046, BJ = 857, GD = 1046, XH = 302986, SJZL = 33360, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 35854, SB = 1194, GJ = 978, MZ = 978, PF = 978, KB = 1194, BJ = 978, GD = 1194, XH = 344770, SJZL = 37931, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 40606, SB = 1352, GJ = 1108, MZ = 1108, PF = 1108, KB = 1352, BJ = 1108, GD = 1352, XH = 390172, SJZL = 42829, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 45677, SB = 1521, GJ = 1246, MZ = 1246, PF = 1246, KB = 1521, BJ = 1246, GD = 1521, XH = 439352, SJZL = 48048, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 51061, SB = 1700, GJ = 1393, MZ = 1393, PF = 1393, KB = 1700, BJ = 1393, GD = 1700, XH = 492453, SJZL = 53592, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 56753, SB = 1890, GJ = 1548, MZ = 1548, PF = 1548, KB = 1890, BJ = 1548, GD = 1890, XH = 549603, SJZL = 59456, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 62747, SB = 2090, GJ = 1711, MZ = 1711, PF = 1711, KB = 2090, BJ = 1711, GD = 2090, XH = 610913, SJZL = 65626, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 69037, SB = 2300, GJ = 1883, MZ = 1883, PF = 1883, KB = 2300, BJ = 1883, GD = 2300, XH = 676480, SJZL = 72118, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 75616, SB = 2519, GJ = 2062, MZ = 2062, PF = 2062, KB = 2519, BJ = 2062, GD = 2519, XH = 746386, SJZL = 78886, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 82479, SB = 2748, GJ = 2249, MZ = 2249, PF = 2249, KB = 2748, BJ = 2249, GD = 2748, XH = 820698, SJZL = 85956, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 89620, SB = 2986, GJ = 2444, MZ = 2444, PF = 2444, KB = 2986, BJ = 2444, GD = 2986, XH = 899469, SJZL = 93318, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 97033, SB = 3233, GJ = 2646, MZ = 2646, PF = 2646, KB = 3233, BJ = 2646, GD = 3233, XH = 982737, SJZL = 100951, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 104712, SB = 3489, GJ = 2855, MZ = 2855, PF = 2855, KB = 3489, BJ = 2855, GD = 3489, XH = 1070529, SJZL = 108856, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 112651, SB = 3754, GJ = 3072, MZ = 3072, PF = 3072, KB = 3754, BJ = 3072, GD = 3754, XH = 1162856, SJZL = 117048, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 120844, SB = 4027, GJ = 3295, MZ = 3295, PF = 3295, KB = 4027, BJ = 3295, GD = 4027, XH = 1259717, SJZL = 125481, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 129286, SB = 4308, GJ = 3525, MZ = 3525, PF = 3525, KB = 4308, BJ = 3525, GD = 4308, XH = 1361098, SJZL = 134170, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 137970, SB = 4597, GJ = 3762, MZ = 3762, PF = 3762, KB = 4597, BJ = 3762, GD = 4597, XH = 1466973, SJZL = 143114, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 146891, SB = 4894, GJ = 4005, MZ = 4005, PF = 4005, KB = 4894, BJ = 4005, GD = 4894, XH = 1577303, SJZL = 152296, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 156043, SB = 5199, GJ = 4255, MZ = 4255, PF = 4255, KB = 5199, BJ = 4255, GD = 5199, XH = 1692038, SJZL = 161731, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 165420, SB = 5512, GJ = 4511, MZ = 4511, PF = 4511, KB = 5512, BJ = 4511, GD = 5512, XH = 1811117, SJZL = 171402, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 寒冰真气
            model = new DataModel();
            model.NAME = "hbzq";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 2617, SB = 104, GJ = 98, MZ = 111, PF = 89, KB = 126, BJ = 123, GD = 35, XH = 23069, SJZL = 3779, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 3140, SB = 125, GJ = 118, MZ = 133, PF = 107, KB = 151, BJ = 148, GD = 42, XH = 34135, SJZL = 4438, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 3900, SB = 155, GJ = 146, MZ = 165, PF = 133, KB = 188, BJ = 184, GD = 52, XH = 45665, SJZL = 5389, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 4892, SB = 195, GJ = 183, MZ = 207, PF = 167, KB = 236, BJ = 231, GD = 65, XH = 57818, SJZL = 6635, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 6114, SB = 244, GJ = 229, MZ = 259, PF = 208, KB = 295, BJ = 288, GD = 81, XH = 70742, SJZL = 8165, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 7562, SB = 302, GJ = 283, MZ = 321, PF = 257, KB = 365, BJ = 356, GD = 100, XH = 84578, SJZL = 9980, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 9233, SB = 369, GJ = 346, MZ = 392, PF = 314, KB = 446, BJ = 435, GD = 122, XH = 99456, SJZL = 12082, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 11122, SB = 444, GJ = 417, MZ = 472, PF = 378, KB = 537, BJ = 524, GD = 147, XH = 115500, SJZL = 14448, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 13226, SB = 528, GJ = 496, MZ = 562, PF = 449, KB = 638, BJ = 623, GD = 175, XH = 132823, SJZL = 17085, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 15542, SB = 620, GJ = 583, MZ = 661, PF = 528, KB = 750, BJ = 732, GD = 206, XH = 151533, SJZL = 19994, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 18065, SB = 721, GJ = 678, MZ = 768, PF = 614, KB = 872, BJ = 851, GD = 240, XH = 171727, SJZL = 23166, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 20792, SB = 830, GJ = 780, MZ = 884, PF = 707, KB = 1003, BJ = 979, GD = 277, XH = 193495, SJZL = 26586, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 23720, SB = 947, GJ = 890, MZ = 1009, PF = 806, KB = 1144, BJ = 1117, GD = 316, XH = 216921, SJZL = 30260, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 26845, SB = 1072, GJ = 1007, MZ = 1142, PF = 912, KB = 1295, BJ = 1264, GD = 358, XH = 242077, SJZL = 34182, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 30163, SB = 1204, GJ = 1131, MZ = 1283, PF = 1025, KB = 1455, BJ = 1420, GD = 402, XH = 269032, SJZL = 38339, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 33670, SB = 1344, GJ = 1263, MZ = 1432, PF = 1144, KB = 1624, BJ = 1585, GD = 449, XH = 297844, SJZL = 42740, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 37363, SB = 1491, GJ = 1401, MZ = 1589, PF = 1269, KB = 1802, BJ = 1759, GD = 498, XH = 328568, SJZL = 47365, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 41238, SB = 1646, GJ = 1546, MZ = 1754, PF = 1400, KB = 1989, BJ = 1941, GD = 550, XH = 361247, SJZL = 52224, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 45291, SB = 1808, GJ = 1698, MZ = 1927, PF = 1538, KB = 2184, BJ = 2132, GD = 604, XH = 395920, SJZL = 57311, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 49520, SB = 1977, GJ = 1857, MZ = 2107, PF = 1681, KB = 2388, BJ = 2331, GD = 661, XH = 432619, SJZL = 62619, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 53920, SB = 2152, GJ = 2022, MZ = 2294, PF = 1830, KB = 2600, BJ = 2538, GD = 720, XH = 471368, SJZL = 68134, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 58488, SB = 2334, GJ = 2193, MZ = 2488, PF = 1985, KB = 2820, BJ = 2753, GD = 781, XH = 512186, SJZL = 73858, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 63220, SB = 2523, GJ = 2370, MZ = 2689, PF = 2146, KB = 3048, BJ = 2976, GD = 844, XH = 555084, SJZL = 79792, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 68112, SB = 2718, GJ = 2553, MZ = 2897, PF = 2312, KB = 3284, BJ = 3206, GD = 910, XH = 600069, SJZL = 85926, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 73160, SB = 2919, GJ = 2742, MZ = 3112, PF = 2483, KB = 3527, BJ = 3443, GD = 978, XH = 647139, SJZL = 92252, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 78361, SB = 3126, GJ = 2937, MZ = 3333, PF = 2659, KB = 3778, BJ = 3688, GD = 1048, XH = 696288, SJZL = 98774, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 83712, SB = 3339, GJ = 3138, MZ = 3561, PF = 2841, KB = 4036, BJ = 3940, GD = 1120, XH = 747504, SJZL = 105491, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 89209, SB = 3558, GJ = 3344, MZ = 3795, PF = 3028, KB = 4301, BJ = 4198, GD = 1194, XH = 800769, SJZL = 112385, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 94848, SB = 3783, GJ = 3555, MZ = 4035, PF = 3219, KB = 4573, BJ = 4463, GD = 1270, XH = 856059, SJZL = 119456, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 100626, SB = 4013, GJ = 3772, MZ = 4281, PF = 3415, KB = 4852, BJ = 4735, GD = 1347, XH = 913346, SJZL = 126704, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 黑血神针
            model = new DataModel();
            model.NAME = "hxsz";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 797, SB = 36, GJ = 33, MZ = 44, PF = 37, KB = 31, BJ = 12, GD = 36, XH = 23069, SJZL = 1583, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 1278, SB = 58, GJ = 53, MZ = 71, PF = 60, KB = 50, BJ = 19, GD = 58, XH = 34135, SJZL = 2243, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 1976, SB = 90, GJ = 82, MZ = 110, PF = 93, KB = 77, BJ = 30, GD = 89, XH = 45665, SJZL = 3196, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 2888, SB = 131, GJ = 120, MZ = 161, PF = 136, KB = 112, BJ = 44, GD = 130, XH = 57818, SJZL = 4438, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 4011, SB = 182, GJ = 167, MZ = 224, PF = 189, KB = 155, BJ = 61, GD = 181, XH = 70742, SJZL = 5972, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 5342, SB = 243, GJ = 222, MZ = 298, PF = 252, KB = 206, BJ = 82, GD = 241, XH = 84578, SJZL = 7789, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 6878, SB = 313, GJ = 286, MZ = 384, PF = 324, KB = 265, BJ = 106, GD = 310, XH = 99456, SJZL = 9885, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 8615, SB = 392, GJ = 358, MZ = 481, PF = 406, KB = 332, BJ = 133, GD = 388, XH = 115500, SJZL = 12255, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 10549, SB = 480, GJ = 439, MZ = 589, PF = 497, KB = 407, BJ = 163, GD = 475, XH = 132823, SJZL = 14898, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 12678, SB = 577, GJ = 528, MZ = 708, PF = 597, KB = 489, BJ = 196, GD = 571, XH = 151533, SJZL = 17806, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 14997, SB = 682, GJ = 625, MZ = 837, PF = 706, KB = 579, BJ = 232, GD = 676, XH = 171727, SJZL = 20973, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 17504, SB = 796, GJ = 729, MZ = 977, PF = 824, KB = 676, BJ = 271, GD = 789, XH = 193495, SJZL = 24395, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 20195, SB = 918, GJ = 841, MZ = 1127, PF = 950, KB = 780, BJ = 313, GD = 910, XH = 216921, SJZL = 28064, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 23067, SB = 1049, GJ = 961, MZ = 1287, PF = 1085, KB = 891, BJ = 358, GD = 1039, XH = 242077, SJZL = 31986, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 26117, SB = 1188, GJ = 1088, MZ = 1457, PF = 1228, KB = 1009, BJ = 405, GD = 1176, XH = 269032, SJZL = 36146, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 29341, SB = 1335, GJ = 1222, MZ = 1637, PF = 1379, KB = 1134, BJ = 455, GD = 1321, XH = 297844, SJZL = 40545, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 32736, SB = 1489, GJ = 1363, MZ = 1826, PF = 1538, KB = 1265, BJ = 508, GD = 1474, XH = 328568, SJZL = 45172, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 36298, SB = 1651, GJ = 1511, MZ = 2024, PF = 1705, KB = 1403, BJ = 563, GD = 1635, XH = 361247, SJZL = 50030, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 40024, SB = 1820, GJ = 1666, MZ = 2231, PF = 1880, KB = 1547, BJ = 621, GD = 1803, XH = 395920, SJZL = 55110, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 43911, SB = 1997, GJ = 1828, MZ = 2447, PF = 2063, KB = 1697, BJ = 681, GD = 1978, XH = 432619, SJZL = 60411, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 47956, SB = 2181, GJ = 1997, MZ = 2672, PF = 2253, KB = 1853, BJ = 744, GD = 2160, XH = 471368, SJZL = 65931, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 52155, SB = 2372, GJ = 2172, MZ = 2906, PF = 2450, KB = 2015, BJ = 809, GD = 2349, XH = 512186, SJZL = 71657, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 56505, SB = 2570, GJ = 2353, MZ = 3148, PF = 2654, KB = 2183, BJ = 877, GD = 2545, XH = 555084, SJZL = 77591, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 61002, SB = 2774, GJ = 2540, MZ = 3398, PF = 2865, KB = 2357, BJ = 947, GD = 2748, XH = 600069, SJZL = 83724, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 65642, SB = 2985, GJ = 2733, MZ = 3656, PF = 3083, KB = 2536, BJ = 1019, GD = 2957, XH = 647139, SJZL = 90050, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 70423, SB = 3202, GJ = 2932, MZ = 3922, PF = 3308, KB = 2721, BJ = 1093, GD = 3173, XH = 696288, SJZL = 96574, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 75342, SB = 3426, GJ = 3137, MZ = 4196, PF = 3539, KB = 2911, BJ = 1169, GD = 3395, XH = 747504, SJZL = 103287, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 80395, SB = 3656, GJ = 3348, MZ = 4477, PF = 3776, KB = 3106, BJ = 1247, GD = 3623, XH = 800769, SJZL = 110180, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 85578, SB = 3892, GJ = 3564, MZ = 4766, PF = 4019, KB = 3306, BJ = 1328, GD = 3857, XH = 856059, SJZL = 117256, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 90889, SB = 4133, GJ = 3785, MZ = 5062, PF = 4268, KB = 3511, BJ = 1410, GD = 4096, XH = 913346, SJZL = 124494, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 七弦无极剑
            model = new DataModel();
            model.NAME = "qxwjj";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 2715, SB = 140, GJ = 93, MZ = 135, PF = 64, KB = 33, BJ = 80, GD = 139, XH = 23069, SJZL = 3782, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 3258, SB = 168, GJ = 112, MZ = 162, PF = 77, KB = 40, BJ = 96, GD = 167, XH = 34135, SJZL = 4444, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 4046, SB = 209, GJ = 139, MZ = 201, PF = 95, KB = 50, BJ = 119, GD = 207, XH = 45665, SJZL = 5394, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 5076, SB = 262, GJ = 174, MZ = 252, PF = 119, KB = 63, BJ = 149, GD = 260, XH = 57818, SJZL = 6637, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 6344, SB = 327, GJ = 217, MZ = 315, PF = 149, KB = 78, BJ = 186, GD = 325, XH = 70742, SJZL = 8163, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 7847, SB = 405, GJ = 269, MZ = 390, PF = 184, KB = 96, BJ = 230, GD = 402, XH = 84578, SJZL = 9982, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 9581, SB = 494, GJ = 328, MZ = 476, PF = 225, KB = 117, BJ = 281, GD = 491, XH = 99456, SJZL = 12074, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 11541, SB = 595, GJ = 395, MZ = 573, PF = 271, KB = 141, BJ = 339, GD = 592, XH = 115500, SJZL = 14443, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 13725, SB = 708, GJ = 470, MZ = 681, PF = 322, KB = 168, BJ = 403, GD = 704, XH = 132823, SJZL = 17082, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 16128, SB = 832, GJ = 552, MZ = 800, PF = 378, KB = 197, BJ = 474, GD = 827, XH = 151533, SJZL = 19981, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 18746, SB = 967, GJ = 642, MZ = 930, PF = 439, KB = 229, BJ = 551, GD = 961, XH = 171727, SJZL = 23143, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 21576, SB = 1113, GJ = 739, MZ = 1071, PF = 505, KB = 263, BJ = 634, GD = 1106, XH = 193495, SJZL = 26560, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 24614, SB = 1270, GJ = 843, MZ = 1222, PF = 576, KB = 300, BJ = 724, GD = 1262, XH = 216921, SJZL = 30234, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 27856, SB = 1437, GJ = 954, MZ = 1383, PF = 652, KB = 339, BJ = 820, GD = 1428, XH = 242077, SJZL = 34150, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 31299, SB = 1615, GJ = 1072, MZ = 1554, PF = 733, KB = 381, BJ = 922, GD = 1605, XH = 269032, SJZL = 38317, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 34938, SB = 1803, GJ = 1197, MZ = 1735, PF = 818, KB = 425, BJ = 1029, GD = 1792, XH = 297844, SJZL = 42717, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 38770, SB = 2001, GJ = 1328, MZ = 1925, PF = 908, KB = 472, BJ = 1142, GD = 1989, XH = 328568, SJZL = 47351, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 42791, SB = 2208, GJ = 1466, MZ = 2125, PF = 1002, KB = 521, BJ = 1261, GD = 2195, XH = 361247, SJZL = 52211, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 46997, SB = 2425, GJ = 1610, MZ = 2334, PF = 1100, KB = 572, BJ = 1385, GD = 2411, XH = 395920, SJZL = 57292, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 51385, SB = 2651, GJ = 1760, MZ = 2552, PF = 1203, KB = 625, BJ = 1514, GD = 2636, XH = 432619, SJZL = 62589, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 55951, SB = 2887, GJ = 1917, MZ = 2779, PF = 1310, KB = 681, BJ = 1649, GD = 2870, XH = 471368, SJZL = 68115, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 60691, SB = 3132, GJ = 2080, MZ = 3014, PF = 1421, KB = 739, BJ = 1789, GD = 3113, XH = 512186, SJZL = 73848, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 65601, SB = 3385, GJ = 2248, MZ = 3258, PF = 1536, KB = 799, BJ = 1934, GD = 3365, XH = 555084, SJZL = 79782, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 70677, SB = 3647, GJ = 2422, MZ = 3510, PF = 1655, KB = 861, BJ = 2084, GD = 3625, XH = 600069, SJZL = 85918, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 75915, SB = 3917, GJ = 2602, MZ = 3770, PF = 1778, KB = 925, BJ = 2238, GD = 3894, XH = 647139, SJZL = 92251, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 81312, SB = 4196, GJ = 2787, MZ = 4038, PF = 1904, KB = 991, BJ = 2397, GD = 4171, XH = 696288, SJZL = 98776, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 86865, SB = 4483, GJ = 2977, MZ = 4314, PF = 2034, KB = 1059, BJ = 2561, GD = 4456, XH = 747504, SJZL = 105491, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 92569, SB = 4777, GJ = 3173, MZ = 4597, PF = 2167, KB = 1128, BJ = 2729, GD = 4749, XH = 800769, SJZL = 112382, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 98420, SB = 5079, GJ = 3374, MZ = 4888, PF = 2304, KB = 1199, BJ = 2902, GD = 5049, XH = 856059, SJZL = 119458, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 104415, SB = 5388, GJ = 3580, MZ = 5186, PF = 2444, KB = 1272, BJ = 3079, GD = 5356, XH = 913346, SJZL = 126703, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 千相正阳掌
            model = new DataModel();
            model.NAME = "qxzyz";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 940, SB = 30, GJ = 28, MZ = 42, PF = 24, KB = 42, BJ = 22, GD = 38, XH = 23069, SJZL = 1590, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 1507, SB = 48, GJ = 45, MZ = 68, PF = 38, KB = 67, BJ = 35, GD = 61, XH = 34135, SJZL = 2245, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 2330, SB = 74, GJ = 70, MZ = 105, PF = 59, KB = 103, BJ = 54, GD = 95, XH = 45665, SJZL = 3199, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 3406, SB = 108, GJ = 102, MZ = 154, PF = 86, KB = 151, BJ = 79, GD = 139, XH = 57818, SJZL = 4447, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 4731, SB = 150, GJ = 141, MZ = 214, PF = 119, KB = 210, BJ = 110, GD = 193, XH = 70742, SJZL = 5978, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 6301, SB = 200, GJ = 188, MZ = 285, PF = 158, KB = 279, BJ = 147, GD = 257, XH = 84578, SJZL = 7795, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 8112, SB = 257, GJ = 242, MZ = 367, PF = 203, KB = 359, BJ = 190, GD = 331, XH = 99456, SJZL = 9891, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 10160, SB = 322, GJ = 303, MZ = 459, PF = 254, KB = 450, BJ = 238, GD = 414, XH = 115500, SJZL = 12257, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 12441, SB = 394, GJ = 371, MZ = 562, PF = 311, KB = 551, BJ = 292, GD = 507, XH = 132823, SJZL = 14897, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 14951, SB = 473, GJ = 446, MZ = 675, PF = 374, KB = 662, BJ = 351, GD = 609, XH = 151533, SJZL = 17799, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 17686, SB = 559, GJ = 527, MZ = 798, PF = 443, KB = 783, BJ = 416, GD = 720, XH = 171727, SJZL = 20960, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 20642, SB = 652, GJ = 615, MZ = 931, PF = 517, KB = 914, BJ = 486, GD = 840, XH = 193495, SJZL = 24376, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 23816, SB = 752, GJ = 710, MZ = 1074, PF = 597, KB = 1054, BJ = 561, GD = 969, XH = 216921, SJZL = 28048, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 27203, SB = 859, GJ = 811, MZ = 1227, PF = 682, KB = 1204, BJ = 641, GD = 1107, XH = 242077, SJZL = 31970, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 30799, SB = 973, GJ = 918, MZ = 1389, PF = 772, KB = 1363, BJ = 726, GD = 1253, XH = 269032, SJZL = 36128, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 34601, SB = 1093, GJ = 1031, MZ = 1561, PF = 867, KB = 1531, BJ = 816, GD = 1408, XH = 297844, SJZL = 40527, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 38604, SB = 1219, GJ = 1150, MZ = 1742, PF = 967, KB = 1708, BJ = 910, GD = 1571, XH = 328568, SJZL = 45153, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 42804, SB = 1352, GJ = 1275, MZ = 1932, PF = 1072, KB = 1894, BJ = 1009, GD = 1742, XH = 361247, SJZL = 50014, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 47198, SB = 1491, GJ = 1406, MZ = 2130, PF = 1182, KB = 2088, BJ = 1113, GD = 1921, XH = 395920, SJZL = 55097, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 51782, SB = 1636, GJ = 1543, MZ = 2337, PF = 1297, KB = 2291, BJ = 1221, GD = 2108, XH = 432619, SJZL = 60406, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 56552, SB = 1787, GJ = 1685, MZ = 2552, PF = 1417, KB = 2502, BJ = 1334, GD = 2302, XH = 471368, SJZL = 65927, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 61503, SB = 1943, GJ = 1833, MZ = 2775, PF = 1541, KB = 2721, BJ = 1451, GD = 2504, XH = 512186, SJZL = 71656, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 66632, SB = 2105, GJ = 1986, MZ = 3007, PF = 1670, KB = 2948, BJ = 1572, GD = 2713, XH = 555084, SJZL = 77596, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 71935, SB = 2272, GJ = 2144, MZ = 3246, PF = 1803, KB = 3183, BJ = 1697, GD = 2929, XH = 600069, SJZL = 83730, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 77407, SB = 2445, GJ = 2307, MZ = 3493, PF = 1940, KB = 3425, BJ = 1826, GD = 3152, XH = 647139, SJZL = 90061, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 83045, SB = 2623, GJ = 2475, MZ = 3747, PF = 2081, KB = 3674, BJ = 1959, GD = 3382, XH = 696288, SJZL = 96581, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 88846, SB = 2806, GJ = 2648, MZ = 4009, PF = 2226, KB = 3931, BJ = 2096, GD = 3618, XH = 747504, SJZL = 103293, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 94805, SB = 2994, GJ = 2826, MZ = 4278, PF = 2375, KB = 4195, BJ = 2237, GD = 3861, XH = 800769, SJZL = 110192, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 100918, SB = 3187, GJ = 3008, MZ = 4554, PF = 2528, KB = 4465, BJ = 2381, GD = 4110, XH = 856059, SJZL = 117261, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 107181, SB = 3385, GJ = 3195, MZ = 4837, PF = 2685, KB = 4742, BJ = 2529, GD = 4365, XH = 913346, SJZL = 124511, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 太极剑法
            model = new DataModel();
            model.NAME = "tjjf";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 5551, SB = 148, GJ = 204, MZ = 124, PF = 273, KB = 77, BJ = 249, GD = 274, XH = 37067, SJZL = 7026, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 6294, SB = 168, GJ = 231, MZ = 141, PF = 309, KB = 87, BJ = 282, GD = 311, XH = 55171, SJZL = 7897, ZXW = 296536 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 7374, SB = 197, GJ = 271, MZ = 165, PF = 362, KB = 102, BJ = 330, GD = 364, XH = 74351, SJZL = 9165, ZXW = 737904 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 8786, SB = 235, GJ = 323, MZ = 197, PF = 431, KB = 122, BJ = 393, GD = 434, XH = 94944, SJZL = 10828, ZXW = 1332712 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 10524, SB = 281, GJ = 387, MZ = 236, PF = 516, KB = 146, BJ = 471, GD = 520, XH = 117267, SJZL = 12870, ZXW = 2092264 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 12584, SB = 336, GJ = 463, MZ = 282, PF = 617, KB = 175, BJ = 563, GD = 622, XH = 141618, SJZL = 15293, ZXW = 3030400 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 14960, SB = 399, GJ = 550, MZ = 335, PF = 734, KB = 208, BJ = 670, GD = 739, XH = 168279, SJZL = 18084, ZXW = 4163344 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 17648, SB = 471, GJ = 649, MZ = 395, PF = 866, KB = 245, BJ = 791, GD = 872, XH = 197511, SJZL = 21247, ZXW = 5509576 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 20642, SB = 551, GJ = 759, MZ = 462, PF = 1013, KB = 287, BJ = 925, GD = 1020, XH = 229558, SJZL = 24768, ZXW = 7089664 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 23936, SB = 639, GJ = 880, MZ = 536, PF = 1175, KB = 333, BJ = 1073, GD = 1183, XH = 264647, SJZL = 28646, ZXW = 8926128 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 27525, SB = 735, GJ = 1012, MZ = 616, PF = 1351, KB = 383, BJ = 1234, GD = 1360, XH = 302986, SJZL = 32864, ZXW = 11043304 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 31405, SB = 838, GJ = 1154, MZ = 703, PF = 1541, KB = 437, BJ = 1408, GD = 1552, XH = 344770, SJZL = 37421, ZXW = 13467192 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 35570, SB = 949, GJ = 1307, MZ = 796, PF = 1745, KB = 495, BJ = 1595, GD = 1758, XH = 390172, SJZL = 42316, ZXW = 16225352 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 40015, SB = 1068, GJ = 1470, MZ = 896, PF = 1963, KB = 557, BJ = 1794, GD = 1978, XH = 439352, SJZL = 47544, ZXW = 19346728 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 44734, SB = 1194, GJ = 1643, MZ = 1002, PF = 2195, KB = 623, BJ = 2006, GD = 2211, XH = 492453, SJZL = 53096, ZXW = 22861544 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 49723, SB = 1327, GJ = 1826, MZ = 1114, PF = 2440, KB = 693, BJ = 2230, GD = 2457, XH = 549603, SJZL = 58962, ZXW = 26801168 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 54976, SB = 1467, GJ = 2019, MZ = 1232, PF = 2698, KB = 766, BJ = 2466, GD = 2716, XH = 610913, SJZL = 65139, ZXW = 31197992 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 60489, SB = 1614, GJ = 2221, MZ = 1356, PF = 2969, KB = 843, BJ = 2713, GD = 2988, XH = 676480, SJZL = 71619, ZXW = 36085296 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 66255, SB = 1768, GJ = 2432, MZ = 1485, PF = 3252, KB = 923, BJ = 2972, GD = 3273, XH = 746386, SJZL = 78395, ZXW = 41497136 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 72271, SB = 1928, GJ = 2653, MZ = 1620, PF = 3547, KB = 1007, BJ = 3242, GD = 3570, XH = 820698, SJZL = 85467, ZXW = 47468224 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 78531, SB = 2095, GJ = 2883, MZ = 1760, PF = 3854, KB = 1094, BJ = 3523, GD = 3879, XH = 899469, SJZL = 92824, ZXW = 54033808 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 85029, SB = 2268, GJ = 3121, MZ = 1906, PF = 4173, KB = 1185, BJ = 3814, GD = 4200, XH = 982737, SJZL = 100461, ZXW = 61229560 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 91760, SB = 2447, GJ = 3368, MZ = 2057, PF = 4503, KB = 1279, BJ = 4116, GD = 4532, XH = 1070529, SJZL = 108370, ZXW = 69075456 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 98719, SB = 2633, GJ = 3623, MZ = 2213, PF = 4845, KB = 1376, BJ = 4428, GD = 4876, XH = 1162856, SJZL = 116553, ZXW = 77655688 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 105901, SB = 2825, GJ = 3886, MZ = 2374, PF = 5198, KB = 1476, BJ = 4750, GD = 5231, XH = 1259717, SJZL = 124997, ZXW = 86958536 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 113300, SB = 3022, GJ = 4157, MZ = 2540, PF = 5561, KB = 1579, BJ = 5082, GD = 5596, XH = 1361098, SJZL = 133689, ZXW = 97036272 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 120912, SB = 3225, GJ = 4436, MZ = 2711, PF = 5935, KB = 1685, BJ = 5423, GD = 5972, XH = 1466973, SJZL = 142637, ZXW = 107925056 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 128731, SB = 3434, GJ = 4723, MZ = 2886, PF = 6319, KB = 1794, BJ = 5774, GD = 6358, XH = 1577303, SJZL = 151831, ZXW = 119660840 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 136753, SB = 3648, GJ = 5017, MZ = 3066, PF = 6713, KB = 1906, BJ = 6134, GD = 6754, XH = 1692038, SJZL = 161262, ZXW = 132279264 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 144972, SB = 3867, GJ = 5318, MZ = 3250, PF = 7116, KB = 2021, BJ = 6503, GD = 7160, XH = 1811117, SJZL = 170921, ZXW = 145815568 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 太岳三青峰
            model = new DataModel();
            model.NAME = "tysqf";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 725, SB = 17, GJ = 42, MZ = 22, PF = 24, KB = 23, BJ = 55, GD = 48, XH = 23069, SJZL = 1588, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 1162, SB = 28, GJ = 67, MZ = 35, PF = 38, KB = 37, BJ = 88, GD = 77, XH = 34135, SJZL = 2242, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 1797, SB = 43, GJ = 103, MZ = 54, PF = 59, KB = 57, BJ = 137, GD = 119, XH = 45665, SJZL = 3191, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 2627, SB = 63, GJ = 151, MZ = 79, PF = 86, KB = 83, BJ = 200, GD = 174, XH = 57818, SJZL = 4434, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 3649, SB = 88, GJ = 210, MZ = 110, PF = 120, KB = 115, BJ = 278, GD = 242, XH = 70742, SJZL = 5971, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 4860, SB = 117, GJ = 280, MZ = 147, PF = 160, KB = 153, BJ = 371, GD = 322, XH = 84578, SJZL = 7791, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 6257, SB = 151, GJ = 360, MZ = 190, PF = 206, KB = 197, BJ = 478, GD = 414, XH = 99456, SJZL = 9888, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 7837, SB = 189, GJ = 451, MZ = 238, PF = 258, KB = 247, BJ = 599, GD = 519, XH = 115500, SJZL = 12262, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 9597, SB = 231, GJ = 552, MZ = 292, PF = 316, KB = 302, BJ = 733, GD = 635, XH = 132823, SJZL = 14896, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 11533, SB = 278, GJ = 663, MZ = 351, PF = 380, KB = 363, BJ = 881, GD = 763, XH = 151533, SJZL = 17802, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 13643, SB = 329, GJ = 784, MZ = 415, PF = 449, KB = 429, BJ = 1042, GD = 903, XH = 171727, SJZL = 20963, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 15923, SB = 384, GJ = 915, MZ = 485, PF = 524, KB = 501, BJ = 1216, GD = 1054, XH = 193495, SJZL = 24386, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 18371, SB = 443, GJ = 1056, MZ = 560, PF = 604, KB = 578, BJ = 1403, GD = 1216, XH = 216921, SJZL = 28059, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 20984, SB = 506, GJ = 1206, MZ = 640, PF = 690, KB = 660, BJ = 1603, GD = 1389, XH = 242077, SJZL = 31980, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 23758, SB = 573, GJ = 1365, MZ = 725, PF = 781, KB = 747, BJ = 1815, GD = 1573, XH = 269032, SJZL = 36142, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 26691, SB = 644, GJ = 1533, MZ = 815, PF = 877, KB = 839, BJ = 2039, GD = 1767, XH = 297844, SJZL = 40538, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 29779, SB = 718, GJ = 1710, MZ = 909, PF = 979, KB = 936, BJ = 2275, GD = 1971, XH = 328568, SJZL = 45166, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 33019, SB = 796, GJ = 1896, MZ = 1008, PF = 1085, KB = 1038, BJ = 2522, GD = 2185, XH = 361247, SJZL = 50020, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 36408, SB = 878, GJ = 2091, MZ = 1112, PF = 1196, KB = 1145, BJ = 2781, GD = 2409, XH = 395920, SJZL = 55108, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 39944, SB = 963, GJ = 2294, MZ = 1220, PF = 1312, KB = 1256, BJ = 3051, GD = 2643, XH = 432619, SJZL = 60408, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 43623, SB = 1052, GJ = 2505, MZ = 1332, PF = 1433, KB = 1372, BJ = 3332, GD = 2887, XH = 471368, SJZL = 65928, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 47442, SB = 1144, GJ = 2724, MZ = 1449, PF = 1559, KB = 1492, BJ = 3624, GD = 3140, XH = 512186, SJZL = 71660, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 51398, SB = 1239, GJ = 2951, MZ = 1570, PF = 1689, KB = 1617, BJ = 3926, GD = 3402, XH = 555084, SJZL = 77594, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 55488, SB = 1337, GJ = 3186, MZ = 1695, PF = 1823, KB = 1746, BJ = 4238, GD = 3673, XH = 600069, SJZL = 83727, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 59709, SB = 1439, GJ = 3428, MZ = 1824, PF = 1962, KB = 1879, BJ = 4560, GD = 3952, XH = 647139, SJZL = 90056, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 64058, SB = 1544, GJ = 3678, MZ = 1957, PF = 2105, KB = 2016, BJ = 4892, GD = 4240, XH = 696288, SJZL = 96583, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 68532, SB = 1652, GJ = 3935, MZ = 2094, PF = 2252, KB = 2157, BJ = 5234, GD = 4536, XH = 747504, SJZL = 103298, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 73128, SB = 1763, GJ = 4199, MZ = 2234, PF = 2403, KB = 2302, BJ = 5585, GD = 4840, XH = 800769, SJZL = 110192, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 77843, SB = 1877, GJ = 4470, MZ = 2378, PF = 2558, KB = 2450, BJ = 5945, GD = 5152, XH = 856059, SJZL = 117265, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 82674, SB = 1993, GJ = 4747, MZ = 2526, PF = 2717, KB = 2602, BJ = 6314, GD = 5472, XH = 913346, SJZL = 124511, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 万花飘零
            model = new DataModel();
            model.NAME = "whpl";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 977, SB = 30, GJ = 29, MZ = 38, PF = 40, KB = 10, BJ = 46, GD = 30, XH = 23069, SJZL = 1585, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 1566, SB = 48, GJ = 46, MZ = 61, PF = 64, KB = 16, BJ = 74, GD = 48, XH = 34135, SJZL = 2236, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 2422, SB = 74, GJ = 71, MZ = 94, PF = 99, KB = 25, BJ = 115, GD = 75, XH = 45665, SJZL = 3188, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 3540, SB = 108, GJ = 104, MZ = 138, PF = 145, KB = 37, BJ = 168, GD = 110, XH = 57818, SJZL = 4435, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 4917, SB = 150, GJ = 144, MZ = 192, PF = 202, KB = 52, BJ = 233, GD = 153, XH = 70742, SJZL = 5968, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 6549, SB = 200, GJ = 192, MZ = 256, PF = 270, KB = 69, BJ = 310, GD = 204, XH = 84578, SJZL = 7788, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 8431, SB = 257, GJ = 247, MZ = 329, PF = 348, KB = 89, BJ = 399, GD = 263, XH = 99456, SJZL = 9881, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 10560, SB = 322, GJ = 309, MZ = 412, PF = 436, KB = 112, BJ = 500, GD = 329, XH = 115500, SJZL = 12250, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 12931, SB = 394, GJ = 378, MZ = 504, PF = 534, KB = 137, BJ = 612, GD = 403, XH = 132823, SJZL = 14882, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 15540, SB = 474, GJ = 454, MZ = 606, PF = 642, KB = 165, BJ = 736, GD = 484, XH = 151533, SJZL = 17789, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 18383, SB = 561, GJ = 537, MZ = 717, PF = 760, KB = 195, BJ = 871, GD = 573, XH = 171727, SJZL = 20958, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 21456, SB = 655, GJ = 627, MZ = 837, PF = 887, KB = 228, BJ = 1016, GD = 669, XH = 193495, SJZL = 24380, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 24755, SB = 756, GJ = 724, MZ = 965, PF = 1024, KB = 263, BJ = 1172, GD = 772, XH = 216921, SJZL = 28055, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 28276, SB = 863, GJ = 827, MZ = 1102, PF = 1170, KB = 300, BJ = 1339, GD = 882, XH = 242077, SJZL = 31973, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 32014, SB = 977, GJ = 936, MZ = 1248, PF = 1325, KB = 340, BJ = 1516, GD = 998, XH = 269032, SJZL = 36133, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 35966, SB = 1098, GJ = 1052, MZ = 1402, PF = 1489, KB = 382, BJ = 1703, GD = 1121, XH = 297844, SJZL = 40535, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 40127, SB = 1225, GJ = 1174, MZ = 1564, PF = 1661, KB = 426, BJ = 1900, GD = 1251, XH = 328568, SJZL = 45167, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 44493, SB = 1358, GJ = 1302, MZ = 1734, PF = 1842, KB = 472, BJ = 2107, GD = 1387, XH = 361247, SJZL = 50026, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 49060, SB = 1497, GJ = 1436, MZ = 1912, PF = 2031, KB = 520, BJ = 2323, GD = 1529, XH = 395920, SJZL = 55106, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 53825, SB = 1642, GJ = 1576, MZ = 2098, PF = 2228, KB = 570, BJ = 2549, GD = 1677, XH = 432619, SJZL = 60408, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 58783, SB = 1793, GJ = 1721, MZ = 2291, PF = 2433, KB = 623, BJ = 2784, GD = 1831, XH = 471368, SJZL = 65923, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 63929, SB = 1950, GJ = 1872, MZ = 2491, PF = 2646, KB = 678, BJ = 3028, GD = 1991, XH = 512186, SJZL = 71652, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 69260, SB = 2113, GJ = 2028, MZ = 2699, PF = 2867, KB = 734, BJ = 3280, GD = 2157, XH = 555084, SJZL = 77584, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 74772, SB = 2281, GJ = 2189, MZ = 2914, PF = 3095, KB = 792, BJ = 3541, GD = 2329, XH = 600069, SJZL = 83716, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 80460, SB = 2455, GJ = 2356, MZ = 3135, PF = 3331, KB = 852, BJ = 3810, GD = 2506, XH = 647139, SJZL = 90047, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 86320, SB = 2634, GJ = 2528, MZ = 3363, PF = 3574, KB = 914, BJ = 4087, GD = 2689, XH = 696288, SJZL = 96572, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 92349, SB = 2818, GJ = 2705, MZ = 3598, PF = 3824, KB = 978, BJ = 4372, GD = 2877, XH = 747504, SJZL = 103285, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 98543, SB = 3007, GJ = 2886, MZ = 3839, PF = 4081, KB = 1044, BJ = 4665, GD = 3070, XH = 800769, SJZL = 110179, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 104896, SB = 3201, GJ = 3072, MZ = 4086, PF = 4344, KB = 1111, BJ = 4966, GD = 3268, XH = 856059, SJZL = 117248, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 111406, SB = 3400, GJ = 3263, MZ = 4339, PF = 4614, KB = 1180, BJ = 5274, GD = 3471, XH = 913346, SJZL = 124496, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            #region 真武七截剑
            model = new DataModel();
            model.NAME = "zwqjj";
            model.DETAILS = new List<Detail>();
            #region 添加明细数据
            model.DETAILS.Add(new Detail() { LEVEL = 0, QX = 839, SB = 15, GJ = 34, MZ = 61, PF = 60, KB = 20, BJ = 26, GD = 14, XH = 23069, SJZL = 1595, ZXW = 0 });
            model.DETAILS.Add(new Detail() { LEVEL = 1, QX = 1345, SB = 24, GJ = 54, MZ = 98, PF = 96, KB = 32, BJ = 41, GD = 22, XH = 34135, SJZL = 2247, ZXW = 184552 });
            model.DETAILS.Add(new Detail() { LEVEL = 2, QX = 2080, SB = 37, GJ = 84, MZ = 151, PF = 149, KB = 49, BJ = 63, GD = 34, XH = 45665, SJZL = 3200, ZXW = 457632 });
            model.DETAILS.Add(new Detail() { LEVEL = 3, QX = 3040, SB = 54, GJ = 123, MZ = 220, PF = 218, KB = 71, BJ = 92, GD = 50, XH = 57818, SJZL = 4443, ZXW = 822152 });
            model.DETAILS.Add(new Detail() { LEVEL = 4, QX = 4222, SB = 75, GJ = 171, MZ = 306, PF = 303, KB = 99, BJ = 128, GD = 69, XH = 70742, SJZL = 5980, ZXW = 1284696 });
            model.DETAILS.Add(new Detail() { LEVEL = 5, QX = 5623, SB = 100, GJ = 228, MZ = 407, PF = 404, KB = 132, BJ = 171, GD = 92, XH = 84578, SJZL = 7802, ZXW = 1850632 });
            model.DETAILS.Add(new Detail() { LEVEL = 6, QX = 7239, SB = 128, GJ = 293, MZ = 524, PF = 520, KB = 170, BJ = 220, GD = 118, XH = 99456, SJZL = 9892, ZXW = 2527256 });
            model.DETAILS.Add(new Detail() { LEVEL = 7, QX = 9066, SB = 160, GJ = 367, MZ = 656, PF = 651, KB = 213, BJ = 276, GD = 148, XH = 115500, SJZL = 12263, ZXW = 3322904 });
            model.DETAILS.Add(new Detail() { LEVEL = 8, QX = 11101, SB = 196, GJ = 449, MZ = 803, PF = 797, KB = 260, BJ = 338, GD = 181, XH = 132823, SJZL = 14896, ZXW = 4246904 });
            model.DETAILS.Add(new Detail() { LEVEL = 9, QX = 13341, SB = 235, GJ = 540, MZ = 965, PF = 958, KB = 312, BJ = 406, GD = 217, XH = 151533, SJZL = 17796, ZXW = 5309488 });
            model.DETAILS.Add(new Detail() { LEVEL = 10, QX = 15781, SB = 278, GJ = 639, MZ = 1142, PF = 1133, KB = 369, BJ = 480, GD = 257, XH = 171727, SJZL = 20962, ZXW = 6521752 });
            model.DETAILS.Add(new Detail() { LEVEL = 11, QX = 18419, SB = 324, GJ = 746, MZ = 1333, PF = 1323, KB = 431, BJ = 560, GD = 300, XH = 193495, SJZL = 24385, ZXW = 7895568 });
            model.DETAILS.Add(new Detail() { LEVEL = 12, QX = 21251, SB = 374, GJ = 861, MZ = 1538, PF = 1527, KB = 497, BJ = 646, GD = 346, XH = 216921, SJZL = 28060, ZXW = 9443528 });
            model.DETAILS.Add(new Detail() { LEVEL = 13, QX = 24273, SB = 427, GJ = 983, MZ = 1757, PF = 1744, KB = 568, BJ = 738, GD = 395, XH = 242077, SJZL = 31977, ZXW = 11178896 });
            model.DETAILS.Add(new Detail() { LEVEL = 14, QX = 27482, SB = 484, GJ = 1113, MZ = 1989, PF = 1975, KB = 643, BJ = 836, GD = 447, XH = 269032, SJZL = 36142, ZXW = 13115512 });
            model.DETAILS.Add(new Detail() { LEVEL = 15, QX = 30874, SB = 544, GJ = 1250, MZ = 2235, PF = 2219, KB = 722, BJ = 939, GD = 502, XH = 297844, SJZL = 40541, ZXW = 15267768 });
            model.DETAILS.Add(new Detail() { LEVEL = 16, QX = 34446, SB = 607, GJ = 1395, MZ = 2494, PF = 2476, KB = 805, BJ = 1048, GD = 560, XH = 328568, SJZL = 45177, ZXW = 17650520 });
            model.DETAILS.Add(new Detail() { LEVEL = 17, QX = 38194, SB = 673, GJ = 1547, MZ = 2765, PF = 2745, KB = 892, BJ = 1162, GD = 621, XH = 361247, SJZL = 50034, ZXW = 20279064 });
            model.DETAILS.Add(new Detail() { LEVEL = 18, QX = 42115, SB = 742, GJ = 1706, MZ = 3049, PF = 3027, KB = 983, BJ = 1281, GD = 684, XH = 395920, SJZL = 55114, ZXW = 23169040 });
            model.DETAILS.Add(new Detail() { LEVEL = 19, QX = 46205, SB = 814, GJ = 1872, MZ = 3345, PF = 3321, KB = 1078, BJ = 1406, GD = 750, XH = 432619, SJZL = 60418, ZXW = 26336400 });
            model.DETAILS.Add(new Detail() { LEVEL = 20, QX = 50461, SB = 889, GJ = 2044, MZ = 3653, PF = 3627, KB = 1177, BJ = 1536, GD = 819, XH = 471368, SJZL = 65935, ZXW = 29797352 });
            model.DETAILS.Add(new Detail() { LEVEL = 21, QX = 54879, SB = 967, GJ = 2223, MZ = 3973, PF = 3945, KB = 1280, BJ = 1671, GD = 891, XH = 512186, SJZL = 71670, ZXW = 33568296 });
            model.DETAILS.Add(new Detail() { LEVEL = 22, QX = 59456, SB = 1048, GJ = 2408, MZ = 4304, PF = 4274, KB = 1387, BJ = 1810, GD = 965, XH = 555084, SJZL = 77602, ZXW = 37665784 });
            model.DETAILS.Add(new Detail() { LEVEL = 23, QX = 64188, SB = 1131, GJ = 2600, MZ = 4646, PF = 4614, KB = 1497, BJ = 1954, GD = 1042, XH = 600069, SJZL = 83735, ZXW = 42106456 });
            model.DETAILS.Add(new Detail() { LEVEL = 24, QX = 69071, SB = 1217, GJ = 2798, MZ = 4999, PF = 4965, KB = 1611, BJ = 2103, GD = 1121, XH = 647139, SJZL = 90067, ZXW = 46907008 });
            model.DETAILS.Add(new Detail() { LEVEL = 25, QX = 74102, SB = 1306, GJ = 3002, MZ = 5363, PF = 5327, KB = 1728, BJ = 2256, GD = 1202, XH = 696288, SJZL = 96589, ZXW = 52084120 });
            model.DETAILS.Add(new Detail() { LEVEL = 26, QX = 79278, SB = 1397, GJ = 3211, MZ = 5738, PF = 5699, KB = 1849, BJ = 2414, GD = 1286, XH = 747504, SJZL = 103301, ZXW = 57654424 });
            model.DETAILS.Add(new Detail() { LEVEL = 27, QX = 84595, SB = 1491, GJ = 3426, MZ = 6123, PF = 6081, KB = 1973, BJ = 2576, GD = 1372, XH = 800769, SJZL = 110194, ZXW = 63634456 });
            model.DETAILS.Add(new Detail() { LEVEL = 28, QX = 90049, SB = 1587, GJ = 3647, MZ = 6518, PF = 6473, KB = 2100, BJ = 2742, GD = 1460, XH = 856059, SJZL = 117264, ZXW = 70040608 });
            model.DETAILS.Add(new Detail() { LEVEL = 29, QX = 95637, SB = 1685, GJ = 3873, MZ = 6922, PF = 6875, KB = 2230, BJ = 2912, GD = 1550, XH = 913346, SJZL = 124501, ZXW = 76889080 });
            #endregion
            dataModelList.Add(model);
            #endregion

            this.dataModels = dataModelList;
        }
        #endregion
    }
}