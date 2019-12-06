using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WaveAlgorithms
{
    class Imports
    {

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        //Ñîçäàíèå èìåíîâàííîãî êàíàëà
        [DllImport("kernel32.dll")]
        public static extern int CreateNamedPipe(string lpName,         //ñòðîêà, ñîäåðæàùàÿ èìÿ êàíàëà 
                                            uint dwOpenMode,             //ðåæèì îòêðûòèÿ êàíàëà
                                            uint dwPipeMode,             //ðåæèì ðàáîòû êàíàëà
                                            uint nMaxInstances,          // ìàêñèìàëüíîå êîëè÷åñòâî ðåàëèçàöèé êàíàëà
                                            uint nOutBufferSize,         // ðàçìåð âûõîäíîãî áóôåðà â áàéòàõ
                                            uint nInBufferSize,          // ðàçìåð âõîäíîãî áóôåðà â áàéòàõ
                                            int nDefaultTimeOut,        // âðåìÿ îæèäàíèÿ â ìñ
                                            uint lpSecurityAttributes);   //àäðåñ ñòðóêòóðû çàùèòû

        // Ñîåäèíåíèå ñî ñòîðîíû ñåðâåðíîãî ïðîöåññà
        [DllImport("kernel32.dll")]
        public static extern bool ConnectNamedPipe(int hNamedPipe,         //äåñêðèïòîð êàíàëà
                                            int lpOverlapped);             //ðåæèì îòêðûòèÿ êàíàëà

        // Îòêëþ÷åíèå ñåðâåðíîãî ïðîöåññà îò êëèåíòñêîãî êàíàëà
        [DllImport("kernel32.dll")]
        public static extern bool DisconnectNamedPipe(int hPipe);

        //Îòêðûòèå êàíàëà
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CreateFile(string lpFileName,                                  //ñòðîêà ñ èìåíåì êàíàëà  
                                               Types.EFileAccess dwDesiredAccess,               //ðåæèì äîñòóïà
                                               Types.EFileShare dwShareMode,                    //ðåæèì ñîâìåñòíîãî èñïîëüçîâàíèÿ
                                               int lpSecurityAttributes,                        //äåñêðèïòîð çàùèòû
                                               Types.ECreationDisposition dwCreationDisposition,//ïàðàìåòðû ñîçäàíèÿ
                                               int dwFlagsAndAttributes,                        //àòðèáóòû ôàéëà
                                               int hTemplateFile);                              //èäåíòèôèêàòîð ôàéëà ñ àòðèáóòàìè

        //Çàïèñü äàííûõ â êàíàë
        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(int hFile,                //îïèñàòåëü ðåàëèçàöèè êàíàëà  
                                     byte[] lpBuffer,                 //àäðåñ áóôåðà, äàííûå èç êîòîðîãî áóäóò çàïèñàíû â êàíàë
                                     uint nNumberOfBytesToWrite,      //ðàçìåð áóôåðà
                                     ref uint lpNumberOfBytesWritten, //÷èñëî áàéò, äåéñòâèòåëüíî çàïèñàííûõ â êàíàë
                                     int lpOverlapped);               //çàâèñèò îò ðåæèìà ðàáîòû
        //×òåíèå äàííûõ èç êàíàëà
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(int hFile,                 //îïèñàòåëü ðåàëèçàöèè êàíàëà
                                    byte[] lpBuffer,              //àäðåñ áóôåðà, êóäà áóäóò ïðî÷èòàíû äàííûå èç êàíàëà
                                    uint nNumberOfBytesToRead,    //ðàçìåð áóôåðà
                                    ref uint lpNumberOfBytesRead, //êîëè÷åñòâî äåéñòâèòåëüíî ïðî÷èòàííûõ áàéò èç êàíàëà
                                    int lpOverlapped);         //çàâèñèò îò ðåæèìà ðàáîòû

        // Ôóíêöèÿ, êîòîðàÿ ïðîâåðÿåò, ÷òî äàííûå äåéñòâèòåëüíî çàïèñàëèñü â ìåéëñëîò
        [DllImport("kernel32.dll")]
        public static extern byte FlushFileBuffers(int hPipe);

        //Çàêðûòèå handle 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(int hObject);

    }
}
