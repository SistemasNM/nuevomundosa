Namespace SoporteTecnico
    Public Enum enuTiposDispositivo
        [Todos] = 0
        [Impresoras] = 1
    End Enum

    Public Class Servidores

        Private Const CONST_SP_LISTAR = "usp_qry_ServidoresListar"

        Public Function Listar(ByRef Lista As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
    End Class
    Public Class DispositivosServidor
        Private Const CONST_SP_LISTAR = "usp_qry_DispositivosListar"

        Private mstrServidor As String

        Public Property Servidor() As String
            Get
                Servidor = mstrServidor
            End Get
            Set(ByVal Value As String)
                mstrServidor = Value
            End Set
        End Property

        Public Function Listar(ByRef Lista As DataTable, Optional ByVal penuTipo As enuTiposDispositivo = enuTiposDispositivo.Todos) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim lobjParam() As Object = {"P_TIPO", CInt(penuTipo), "P_SERVIDOR", mstrServidor}

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR, lobjParam)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function

    End Class
    Public Class IniciosSesion
        Private Const CONST_SP_LISTAR = "usp_qry_IniciosSesionListar"

        Public Function Listar(ByRef Lista As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
    End Class
End Namespace
