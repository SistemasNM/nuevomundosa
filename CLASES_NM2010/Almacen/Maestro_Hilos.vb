Imports NM.AccesoDatos
Imports System.Data
Namespace NM_Hilos
    Public Class Maestro_Hilos
#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccHila As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccHila = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub
#End Region
        Public Sub Registra_Hilo(ByVal Codigo_Hilo As String, ByVal Descripcion_Hilo As String, ByVal Usuario As String)
            Dim objParametros As Object() = {"Co_Item", Codigo_Hilo, "De_Item", Descripcion_Hilo, "Usuario", Usuario}
            m_sqlDtAccHila.EjecutarComando("USP_MaestroHilos_Insertar", objParametros)
        End Sub
        'Public Sub Consulta_Hilo(ByVal Tipo_Tabla As String)
        '    Dim objParametros As Object() = {"Tipo_Tabla", Tipo_Tabla}
        '    m_sqlDtAccHila.EjecutarComando("USP_MaestroHilos_Datos_Consultar", objParametros)
        'End Sub
    End Class
End Namespace



