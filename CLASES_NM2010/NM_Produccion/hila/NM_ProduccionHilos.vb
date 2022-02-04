Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos
Imports NM_General

Namespace NM_Hilanderia
    Public Class ParteProduccionHilos
        'Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccHilanderia As AccesoDatosSQLServer
        Private _strUsuario As String
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Produccion_Hilos_Listar(ByVal pFecha As String) As DataTable

            Dim dtblDatos As DataTable
            Try
                Dim objParametros() As Object = {"FechaProd", pFecha}
                dtblDatos = m_sqlDtAccHilanderia.ObtenerDataTable("Usp_Get_Produccion_Hilos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function Produccion_Hilos_Grabar(ByVal strAlmacen As String, _
    ByVal strFecha As String, _
  ByVal strObservacion As String, _
  ByVal strUsuario As String, _
  ByVal dtbProduccion As DataTable) As DataTable
            m_sqlDtAccHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objUtil As New NM_General.Util
                dtbProduccion.TableName = "Produccion_Detalle"
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbProduccion)
                Dim objParametros() As String = {"var_Almacen", strAlmacen, _
                "var_Fecha", strFecha, "var_Observacion", strObservacion, _
                "var_Usuario", _strUsuario, "var_XMLDatos", strXMLDatos}
                Return m_sqlDtAccHilanderia.ObtenerDataTable("Usp_Hil_ParteProduccion_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

    End Class
End Namespace