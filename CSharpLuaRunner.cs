using CSR;
using KeraLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CSharpLuaRunner
{
    class CSharpLuaRunner
    {
        public static Dictionary<string, IntPtr> ptr = new Dictionary<string, IntPtr>();
        public static Dictionary<string, string> ShareDatas = new Dictionary<string, string>();
        public static Dictionary<string, object> ObjectDatas = new Dictionary<string, object>();
        public static string version = "0.0.1";
        public static string localip = "127.0.0.1";

        public class MCLUAAPI
        {
            private MCCSAPI api { get; set; }
            private Dictionary<string, int> TPFuncPtr { get; set; }

            public MCLUAAPI(MCCSAPI api)
            {
                this.api = api;
                TPFuncPtr = new Dictionary<string, int>
                {
                    { "1.16.200.2", 0x00C82C60 },
                    { "1.16.201.2", 0x00C82C60 }
                };
            }

            public string MCLUAAPIVERSION()
            {
                return version;
            }

            public void runCmd(string cmd)
            {
                api.runcmd(cmd);
            }

            public void Log(string msg)
            {
                api.logout(msg);
            }

            public string getOnLinePlayers()
            {
                return api.getOnLinePlayers();
            }

            public string getPlayerPermissionAndGametype(string uuid)
            {
                return api.getPlayerPermissionAndGametype(uuid);
            }

            public string getPlayerAbilities(string uuid)
            {
                return api.getPlayerAbilities(uuid);
            }

            public string getPlayerAttributes(string uuid)
            {
                return api.getPlayerAttributes(uuid);
            }

            public string getPlayerEffects(string uuid)
            {
                return api.getPlayerEffects(uuid);
            }

            public int getscoreboard(string uuid, string name)
            {
                return api.getscoreboard(uuid, name);
            }

            public void addPlayerItem(string uuid, int id, short aux, byte count)
            {
                api.addPlayerItem(uuid, id, aux, count);
            }

            public void setCommandDescribe(string key, string descripition)
            {
                api.setCommandDescribe(key, descripition);
            }

            public void setPlayerBossBart(string uuid, string title, float percent)
            {
                api.setPlayerBossBar(uuid, title, percent);
            }

            public void setPlayerSidebar(string uuid, string title, string list)
            {
                api.setPlayerSidebar(uuid, title, list);
            }

            public void setCommandDescribeEx(string key, string description, MCCSAPI.CommandPermissionLevel level, byte flag1, byte flag2)
            {
                api.setCommandDescribeEx(key, description, level, flag1, flag2);
            }

            public void setPlayerPermissionAndGametype(string uuid, string modes)
            {
                api.setPlayerPermissionAndGametype(uuid, modes);
            }

            public string getPlayerItems(string uuid)
            {
                return api.getPlayerItems(uuid);
            }

            public void disconnectClient(string uuid, string tips)
            {
                api.disconnectClient(uuid, tips);
            }

            public string getPlayerSelectedItem(string uuid)
            {
                return api.getPlayerSelectedItem(uuid);
            }

            public void transferserver(string uuid, string addr, int port)
            {
                api.transferserver(uuid, addr, port);
            }

            public void tellraw(string towho, string msg)
            {
                api.runcmd("tellraw " + towho + " {\"rawtext\":[{\"text\":\"" + msg + "\"}]}");
            }

            public void talkAs(string uuid, string msg)
            {
                api.talkAs(uuid, msg);
            }

            public string MCCSAPIVERSION()
            {
                return api.VERSION;
            }

            public uint sendSimpleForm(string uuid, string title, string contest, string buttons)
            {
                return api.sendSimpleForm(uuid, title, contest, buttons);
            }

            public void releaseForm(uint formid)
            {
                api.releaseForm(formid);
            }

            public void removePlayerBossBar(string uuid)
            {
                api.removePlayerBossBar(uuid);
            }

            public void removePlayerSidebar(string uuid)
            {
                api.removePlayerSidebar(uuid);
            }

            public uint sendCustomForm(string uuid, string json)
            {
                return api.sendCustomForm(uuid, json);
            }

            public uint sendModalForm(string uuid, string title, string contest, string button1, string button2)
            {
                return api.sendModalForm(uuid, title, contest, button1, button2);
            }

            public string getPlayerMaxAttributes(string uuid)
            {
                return api.getPlayerMaxAttributes(uuid);
            }

            public void setServerMotd(string motd, bool isshow)
            {
                api.setServerMotd(motd, isshow);
            }

            public void reName(string uuid, string name)
            {
                api.reNameByUuid(uuid, name);
            }

            public void runCmdAs(string uuid, string command)
            {
                api.runcmdAs(uuid, command);
            }

            public string selectPlayer(string uuid)
            {
                return api.selectPlayer(uuid);
            }

            public void sendText(string uuid, string text)
            {
                api.sendText(uuid, text);
            }

            public void setscoreboard(string uuid, string objname, int count)
            {
                api.setscoreboard(uuid, objname, count);
            }

            public GUI.GUIBuilder creatGUI(string title)
            {
                return new GUI.GUIBuilder(api, title);
            }

            public CsPlayer creatPlayerObject(string uuid)
            {
                try
                {
                    var pl = ptr[uuid];
                    return new CsPlayer(api, pl);
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR][CSLR] " + e.Message);
                    return null;
                }
            }

            public CsActor getActorFromUniqueid(ulong uniqueid)
            {
                return CsActor.getFromUniqueId(api, uniqueid);
            }

            public CsPlayer getPlayerFromUniqueid(ulong uniqueid)
            {
                return (CsPlayer)CsPlayer.getFromUniqueId(api, uniqueid);
            }

            public CsActor[] getFromAABB(int did, float x1, float y1, float z1, float x2, float y2, float z2)
            {
                var temp = new List<CsActor>();
                var raw = CsActor.getsFromAABB(api, did, x1, y1, z2, x2, y2, z2);
                foreach (var i in raw)
                {
                    temp.Add((CsActor)i);
                }
                return temp.ToArray();
            }

            public CsPlayer convertActorToPlayer(CsActor ac)
            {
                return (CsPlayer)ac;
            }

            public void WriteAllText(string path, string contenst)
            {
                File.WriteAllText(path, contenst);
            }

            public void AppendAllText(string path, string contenst)
            {
                File.AppendAllText(path, contenst);
            }

            public string[] ReadAllLine(string path)
            {
                return File.ReadAllLines(path);
            }

            public string ReadAllText(string path)
            {
                return File.ReadAllText(path);
            }

            public string WorkingPath()
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }

            public string ToMD5(string word)
            {
                string md5output = "";
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] date = System.Text.Encoding.Default.GetBytes(word);
                byte[] date1 = md5.ComputeHash(date);
                md5.Clear();
                for (int i = 0; i < date1.Length - 1; i++)
                {
                    md5output += date1[i].ToString("X");
                }
                return md5output;
            }

            public string HttpPost(string Url, string postDataStr)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                /*foreach (KeyValuePair<string , string > kvp in dict)
                    request.Headers.Add(kvp.Key, kvp.Value);*/
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }

            public string HttpGet(string Url, string postDataStr)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }

            public void CreateDir(string path)
            {
                Directory.CreateDirectory(path);
            }

            public bool IfFile(string path)
            {
                return File.Exists(path);
            }

            public bool IfDir(string path)
            {
                return Directory.Exists(path);
            }

            public int ShareData(string key, string value)
            {
                if (!ShareDatas.ContainsKey(key))
                {
                    ShareDatas.Add(key, value);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public string GetShareData(string key)
            {
                if (ShareDatas.ContainsKey(key))
                {
                    return ShareDatas[key];
                }
                else
                {
                    return "1";
                }
            }

            public int ChangeShareData(string key, string value)
            {
                if (ShareDatas.ContainsKey(key))
                {
                    ShareDatas[key] = value;
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public int RemoveShareData(string key)
            {
                if (ShareDatas.ContainsKey(key))
                {
                    ShareDatas.Remove(key);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public string GetLocalIP()
            {
                return localip;
            }

            public void ShareFunc(string key, object func)
            {
                if (!ObjectDatas.ContainsKey(key))
                {
                    ObjectDatas.Add(key, func);

                }
            }

            public object GetShareFunc(string key)
            {
                if (ObjectDatas.ContainsKey(key))
                {
                    return ObjectDatas[key];
                }
                return null;

            }

            public void ThrowException(string msg)
            {
                throw new ArgumentOutOfRangeException(msg);
            }
        }

        public static bool TRY(Action act)
        {
            try
            {
                act.Invoke();
                return true;
            }
            catch (Exception e)
            {
                if (!e.Message.StartsWith("“Microsoft.Scripting.Hosting.ScriptScope”") && e.Message.IndexOf("无法将 null 转换为“bool") == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] [CSLR] " + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return false;
            }
        }

        public static void CallLuaFunc(List<dynamic> Func, Action<dynamic> act)
        {
            foreach (var fun in Func)
            {
                TRY(() =>
                {
                    act(fun);
                });
            }
        }

        static string CsGetUuid(List<IntPtr> pls, string pln, MCCSAPI api)
        {
            foreach (IntPtr pl in pls)
            {
                CsPlayer cpl = new CsPlayer(api, pl);
                if (cpl.getName() == pln)
                {
                    return cpl.Uuid;
                }
            }
            return string.Empty;
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public static void RunCSharpLua(MCCSAPI api)
        {
            List<IntPtr> uuid = new List<IntPtr>();
            const String path = "./cslr";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("[INFO] [CSLR] 已创建文件夹");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[INFO] [CSLR] CSharpLuaRunner加载中");
            if (!File.Exists("./KeraLua.dll"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[IPYR] 无法找到依赖库 请将KeraLua.dll与Lua54.dll放到BDS根目录");
                Console.ForegroundColor = ConsoleColor.White;
            }
            var LuaFun = new List<dynamic>();
            DirectoryInfo Allfolder = new DirectoryInfo(path);
            var mc = new MCLUAAPI(api);
            GC.KeepAlive(mc);
            foreach (FileInfo file in Allfolder.GetFiles("*.cs.lua"))
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("[INFO] [CSLR] 正在加载" + file.Name);
                    Lua lua = new Lua();
                    lua.DoFile(file.FullName);
                    Console.WriteLine("[INFO] [CSLR] " + file.Name + "加载成功");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] [CSLR] " + e.Message);
                    Console.WriteLine("[ERROR] [CSLR] 加载" + file.Name + "失败");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            api.addBeforeActListener(EventKey.onLoadName, x =>
            {
                var a = BaseEvent.getFrom(x) as LoadNameEvent;
                uuid.Add(a.playerPtr);
                ptr.Add(a.uuid, a.playerPtr);
                CallLuaFunc(LuaFun, func =>
                {
                    CsPlayer p = new CsPlayer(api, a.playerPtr);
                    string list = "{\'playername\':\'" + a.playername + "\',\'uuid\':\'" + a.uuid + "\',\'xuid\':\'" + a.xuid + "\',\'IPport\':\'" + p.IpPort + "\'}";
                    var re = func.load_name(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onPlayerLeft, x =>
            {
                var a = BaseEvent.getFrom(x) as PlayerLeftEvent;
                uuid.Remove(a.playerPtr);
                ptr.Remove(a.uuid);
                CallLuaFunc(LuaFun, func =>
                {
                    string list = "{\'playername\':\'" + a.playername + "\',\'uuid\':\'" + a.uuid + "\',\'xuid\':\'" + a.xuid + "\'}";
                    var re = func.player_left(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onServerCmd, x =>
            {
                var a = BaseEvent.getFrom(x) as ServerCmdEvent;

                if (a.cmd.StartsWith("cslr "))
                {
                    string[] sArray = a.cmd.Split(new char[2] { ' ', ' ' });

                    if (sArray[1] == "help")
                    {
                        Console.WriteLine("[INFO] [CSLR] help   使用帮助\n[INFO] [CSLR] info    CSLR信息\n[INFO] [CSLR] list  插件列表\n[INFO] [CSLR] reload  重载插件");
                        return false;
                    }

                    if (sArray[1] == "info")
                    {
                        MessageBox.Show("感谢使用CSharpLuaRunner\n作者:SeaIceNX", "当前版本" + version, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Write("[INFO] [CSLR] 窗体关闭 控制台已恢复");
                        return false;
                    }

                    if (sArray[1] == "list")
                    {
                        DirectoryInfo folder = new DirectoryInfo(path);
                        int total = 0;
                        Console.WriteLine("[INFO] [CSLR] 正在读取插件列表");
                        foreach (FileInfo file in Allfolder.GetFiles("*.cs.lua"))
                        {
                            Console.WriteLine(" - " + file.Name + " | ID: " + total);
                            total += 1;
                        }
                        Console.WriteLine($"[INFO] [CSLR]共加载了{total}个插件");
                        return false;
                    }

                    if (sArray[1] == "reload")
                    {
                        LuaFun.Clear();
                        ShareDatas.Clear();
                        DirectoryInfo folder = new DirectoryInfo(path);
                        foreach (FileInfo file in folder.GetFiles("*.cs.lua"))
                        {
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("[INFO] [CSLR] 正在加载" + file.Name);
                                Lua lua = new Lua();
                                lua.DoFile(file.FullName);
                                Console.WriteLine("[INFO] [CSLR] " + file.Name + "加载成功");
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[ERROR] [CSLR] " + e.Message);
                                Console.WriteLine("[ERROR] [CSLR] 加载" + file.Name + "失败");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.WriteLine("[INFO] [CSLR] 重载成功");
                        return false;
                    }
                    return true;
                }
                else
                {
                    var re = true;
                    CallLuaFunc(LuaFun, func =>
                    {
                        string list = $"{{\'cmd\':\' {a.cmd }\'}}";
                        re = func.server_command(list);
                    });
                    return re;
                }
            });

            api.addBeforeActListener(EventKey.onEquippedArmor, x =>
            {
                var a = BaseEvent.getFrom(x) as EquippedArmorEvent;
                CallLuaFunc(LuaFun, func =>
                {
                    string list = "{\'playername\':\'" + a.playername + "\',\'itemid\':\'" + a.itemid + "\',\'itemname\':\'" + a.itemname + "\',\'itemcount\':\'" + a.itemcount + "\',\'itemaux\':\'" + a.itemaux + "\',\'slot\':\'" + a.slot + "\',\'Pos\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "]}";
                    var re = func.equippedarm(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onAttack, x =>
            {
                var a = BaseEvent.getFrom(x) as AttackEvent;
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    string list = "{\'actorname\':\'" + a.actorname + "\',\'dimensionid\':\'" + a.dimensionid + "\',\'playername\':\'" + a.playername + "\',\'Pos\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "]}";
                    re = func.attack(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onInputText, x =>
            {
                var a = BaseEvent.getFrom(x) as InputTextEvent;
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    string list = "{\'msg\':\'" + a.msg + "\',\'dimensionid\':\'" + a.dimensionid + "\',\'uuid\':\'" + CsGetUuid(uuid, a.playername, api) + "\',\'playername\':\'" + a.playername + "\',\'XYZ\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "]}";
                    re = func.inputtext(list);
                });
                return re;
            });
            api.addBeforeActListener(EventKey.onDestroyBlock, x =>
            {
                var a = BaseEvent.getFrom(x) as DestroyBlockEvent;
                var re = true;
                string list = "{\'blockid\':\'" + a.blockid + "\',\'uuid\':\'" + CsGetUuid(uuid, a.playername, api) + "\',\'position\':[" + Convert.ToInt32(a.position.x) + "," + Convert.ToInt32(a.position.y) + "," + Convert.ToInt32(a.position.z) + "],\'blockname\':\'" + a.blockname + "\',\'dimensionid\':\'" + a.dimensionid + "\',\'playername\':\'" + a.playername + "\',\'Pos\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "]}";
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.destroyblock(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onMobDie, x =>
            {
                var a = BaseEvent.getFrom(x) as MobDieEvent;
                var re = true;
                string list = "{\'mobname\':\'" + a.mobname + "\',\'mobtype\':\'" + a.mobtype + "\',\'XYZ\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "],\'srcname\':\'" + a.srcname + "\',\'dimensionid\':\'" + a.dimensionid + "\',\'playername\':\'" + a.playername + "\'}";
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.mobdie(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onRespawn, x =>
            {
                var a = BaseEvent.getFrom(x) as RespawnEvent;
                string list = "{\'XYZ\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "],\'dimensionid\':\'" + a.dimensionid + "\',\'playername\':\'" + a.playername + "\',\'uuid\':\'" + CsGetUuid(uuid, a.playername, api) + "\'}";

                CallLuaFunc(LuaFun, func =>
                {
                    var re = func.respawn(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onInputCommand, x =>
            {
                var a = BaseEvent.getFrom(x) as InputCommandEvent;
                var re = true;
                string list = "{\'cmd\':\'" + a.cmd + "\',\'XYZ\':[" + Convert.ToInt32(a.XYZ.x) + "," + Convert.ToInt32(a.XYZ.y) + "," + Convert.ToInt32(a.XYZ.z) + "],\'dimensionid\':\'" + a.dimensionid + "\',\'playername\':\'" + a.playername + "\',\'uuid\':\'" + CsGetUuid(uuid, a.playername, api) + "\'}";
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.inputcommand(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onFormSelect, x =>
            {
                var a = BaseEvent.getFrom(x) as FormSelectEvent;
                string list = $"{{\'playername\':\'{a.playername}\',\'selected\':{a.selected},\'uuid\':\'{a.uuid}\',\'formid\':\'{a.formid}\'}}";
                CallLuaFunc(LuaFun, func =>
                {
                    var re = func.formselect(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onUseItem, x =>
            {
                var a = BaseEvent.getFrom(x) as UseItemEvent;
                string list = $"{{\'playername\':\'{a.playername}\',\'itemid\':\'{a.itemid}\',\'itemaux\':\'{a.itemaux}\',\'itemname\':\'{a.itemname}\',\'XYZ\':[{a.XYZ.x},{a.XYZ.y},{a.XYZ.z}],\'postion\':[{a.position.x},{a.position.y},{a.position.z}],\'blockname\':\'{a.blockname}\',\'blockid\':\'{a.blockid}\'}}";
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.useitem(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onPlacedBlock, x =>
            {
                var a = BaseEvent.getFrom(x) as PlacedBlockEvent;
                string list = $"{{\'playername\':\'{a.playername}\',\'blockid\':\'{a.blockid}\',\'blockname\':\'{a.blockname}\',\'XYZ\':[{a.XYZ.x},{a.XYZ.y},{a.XYZ.z}],\'postion\':[{a.position.x},{a.position.y},{a.position.z}],\'dimensionid\':\'{a.dimensionid}\'}}";
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.placeblock(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onLevelExplode, x =>
            {
                var a = BaseEvent.getFrom(x) as LevelExplodeEvent;
                string list = $"{{\'explodepower\':\'{a.explodepower}\',\'blockid\':\'{a.blockid}\',\'blockname\':\'{a.blockname}\',\'entity\':\'{a.entity}\',\'entityid\':\'{a.entityid}\',\'dimensionid\':\'{a.dimensionid}\',\'postion\':[{a.position.x},{a.position.y},{a.position.z}]}}";
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.levelexplode(list);
                });
                return re;

            });

            api.addBeforeActListener(EventKey.onNpcCmd, x =>
            {
                var a = BaseEvent.getFrom(x) as NpcCmdEvent;
                string list = $"{{\'npcname\':\'{a.npcname}\',\'actionid\':\'{a.actionid}\',\'actions\':\'{a.actions}\',\'dimensionid\':\'{a.dimensionid}\',\'entity\':\'{a.entity}\',\'entityid\':\'{a.entityid}\',\'postion\':[{a.position.x},{a.position.y},{a.position.z}]}}";

                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.npccmd(list);
                });
                return re;

            });

            api.addBeforeActListener(EventKey.onBlockCmd, x =>
            {
                var a = BaseEvent.getFrom(x) as BlockCmdEvent;
                string list = $"{{\'cmd\':\'{a.cmd}\',\'dimensionid\':\'{a.dimensionid}\',\'postion\':[{a.position.x},{a.position.y},{a.position.z}],\'type\':\'{a.type}\',\'tickdelay\':\'{a.tickdelay}\'}}";
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.blockcmd(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onPistonPush, x =>
            {
                var a = BaseEvent.getFrom(x) as PistonPushEvent;
                var re = true;
                string list = $"{{\'targetposition\':[{a.targetposition.x},{a.targetposition.y},{a.targetposition.z}],\'blockid\':\'{a.blockid}\',\'blockname\':\'{ a.blockname }\',\'dimensionid\':\'{ a.dimensionid }\',\'targetblockid\':\'{a.targetblockid}\',\'targetblockname\':\'{a.targetblockname}}}";
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.pistonpush(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onStartOpenChest, x =>
            {
                var a = BaseEvent.getFrom(x) as StartOpenChestEvent;
                string list = $"{{\'playername\':\'{a.playername}\',\'blockid\':\'{a.blockid}\',\'blockname\':\'{a.blockname}\',\'XYZ\':[{a.XYZ.x},{a.XYZ.y},{a.XYZ.z}],\'postion\':[{a.position.x},{a.position.y},{a.position.z}],\'dimensionid\':\'{a.dimensionid}\'}}";
                var re = true;
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.openchest(list);
                });
                return re;
            });

            api.addBeforeActListener(EventKey.onStopOpenChest, x =>
            {
                var a = BaseEvent.getFrom(x) as StopOpenChestEvent;
                string list = $"{{\'playername\':\'{a.playername}\',\'blockid\':\'{a.blockid}\',\'blockname\':\'{a.blockname}\',\'XYZ\':[{a.XYZ.x},{a.XYZ.y},{a.XYZ.z}],\'postion\':[{a.position.x},{a.position.y},{a.position.z}],\'dimensionid\':\'{a.dimensionid}\'}}";
                CallLuaFunc(LuaFun, func =>
                {
                    var re = func.closechest(list);
                });
                return true;
            });

            api.addBeforeActListener(EventKey.onServerCmdOutput, x =>
            {
                var a = BaseEvent.getFrom(x) as ServerCmdOutputEvent;
                var re = true;
                string list = $"{{\'output\':\'{a.output.Replace("\n", null).Replace("\'", "\\\'")}\'}}";
                CallLuaFunc(LuaFun, func =>
                {
                    re = func.server_cmdoutput(list);
                });
                return re;
            });
        }
    }
}

namespace CSR
{
    partial class Plugin
    {
        public static void onStart(MCCSAPI api)
        {
            try
            {
                csapi.api = api;
                CSharpLuaRunner.CSharpLuaRunner.RunCSharpLua(api);
                Console.WriteLine("[INFO] [CSLR] CSharpLuaRunner加载成功\n[INFO] [CSLR] 输入cslr help查看帮助");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] [CSLR] " + e.Message);
            }
        }
    }

    public class csapi
    {
        public static MCCSAPI api;
    }
}
