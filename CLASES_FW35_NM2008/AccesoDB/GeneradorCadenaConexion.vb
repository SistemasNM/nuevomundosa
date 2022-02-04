Option Strict On

Imports Microsoft.Win32
Imports System.IO
Imports System.Security

Namespace NM.AccesoDatos
    Public NotInheritable Class GeneradorCadenaConexion
        Public Enum enmBasesDatos As Integer
            RevisionFinal = 0
            LogisticaOfisis = 1
            Produccion = 2
            PlanOfisis = 3
            Hilanderia = 4
            Tintoreria = 5
            CalidadTintoreria = 6
            Costos = 7
            Gandi = 8
            CalidadHilanderia = 9
            FichaCosto = 10
            ContabilidadOfisis = 11
            TesoreriaOfisis = 12
            VentasOfisis = 13
            SeguridadOfisis = 14
            PresupuestoOfisis = 15
            InterfasesOfisis = 16
            ActivoFijoOfisis = 17
            Intranet = 18
            Bonifica = 19
            AuxiliarNM04 = 20
            CostosReales = 21
            CostosTejeduria = 22
            NMSmart = 23
        End Enum

        Private Shared m_strCadenaConexionSQLServer As String = ""


        Public Shared Function ObtenerCadenaConexionSQLServer(ByVal BaseDatos As enmBasesDatos) As String
            Try
                Dim strServidor, strBaseDatos, strUsuario, strPassword As String

                strServidor = ""
                strBaseDatos = ""
                strUsuario = ""
                strPassword = ""

                Select Case BaseDatos
                    Case Is = enmBasesDatos.RevisionFinal
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMREVFIN").GetValue("Servidor"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMREVFIN").GetValue("BaseDatos"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMREVFIN").GetValue("Usuario"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMREVFIN").GetValue("Password"), String)
                    Case Is = enmBasesDatos.LogisticaOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFILOGI").GetValue("Passwd"), String)
                    Case Is = enmBasesDatos.Produccion
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMPROD4").GetValue("Passwd"), String)
                    Case Is = enmBasesDatos.PlanOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPLAN").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Tintoreria
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMTINTO").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.CalidadTintoreria
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALITINTO").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALITINTO").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALITINTO").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALITINTO").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Hilanderia
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMHILA").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Costos
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\COSTOS").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\COSTOS").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\COSTOS").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\COSTOS").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Gandi
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\GANDI").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.CalidadHilanderia
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMCALI").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.FichaCosto
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\FichaCosto").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\FichaCosto").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\FichaCosto").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\FichaCosto").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.ContabilidadOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFICONT").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFICONT").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFICONT").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFICONT").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.TesoreriaOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFITESO").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFITESO").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFITESO").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFITESO").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.VentasOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIVENT").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.SeguridadOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFISEGU").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.PresupuestoOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPRES").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPRES").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPRES").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIPRES").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.InterfasesOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIINTE").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIINTE").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIINTE").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIINTE").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.ActivoFijoOfisis
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIACTI").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIACTI").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIACTI").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\OFIACTI").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Intranet
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\INTRANET").GetValue("Server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\INTRANET").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\INTRANET").GetValue("User"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\INTRANET").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.Bonifica
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\Bonifica").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\Bonifica").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\Bonifica").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\Bonifica").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.AuxiliarNM04
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\AuxNM04").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\AuxNM04").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\AuxNM04").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\AuxNM04").GetValue("passwd"), String)
                    Case Is = enmBasesDatos.CostosReales
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoReales").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoReales").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoReales").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoReales").GetValue("passwd"), String)

                    Case Is = enmBasesDatos.CostosTejeduria
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoTejeduria").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoTejeduria").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoTejeduria").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\CostoTejeduria").GetValue("passwd"), String)

                    Case Is = enmBasesDatos.NMSmart
                        strServidor = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMSMART").GetValue("server"), String)
                        strBaseDatos = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMSMART").GetValue("BD"), String)
                        strUsuario = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMSMART").GetValue("user"), String)
                        strPassword = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\NMSMART").GetValue("passwd"), String)
                End Select

                Return "Data Source=" & strServidor & ";Initial Catalog=" & strBaseDatos & ";User Id=" & strUsuario & ";Pwd=" & strPassword & "; Connect Timeout=200"

            Catch IOEx As IOException
                Throw IOEx
            Catch SecEx As SecurityException
                Throw SecEx
            End Try
        End Function
    End Class
End Namespace