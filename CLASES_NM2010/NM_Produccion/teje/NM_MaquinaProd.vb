Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_MaquinaProd
        Dim mstrError As String = ""

        Public ReadOnly Property clsError() As String
            Get
                Return mstrError
            End Get
        End Property

        '--------------------------------------------------------------------
        'Creado por: Carlos Ponce Taype
        'Fecha     : 00-00-00
        'Proposito : retorna la lista de las maquinas segun la clasificación

        'Modificado: Alexander Torres Cardenas
        'Fecha: Julio 2014
        'Proposito¨: Filtramos segun tipo de urdido y engomado
        'Implementar el engomado crudo en el proceso TED
        '---------------------------------------------------------------------

        Public Function MaquinaListar(ByVal Codigo_ClaseMaq As String, _
                                      ByVal Codigo_Proceso As String, _
                                     ByVal Codigo_Maquina As String, _
                                     ByVal Descri_Maquina As String, _
                                     ByRef pDT As DataTable) As Boolean
            Dim blnRpta As Boolean = False
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"Codigo_ClaseMaq", Codigo_ClaseMaq, _
                                            "Codigo_Proceso", Codigo_Proceso, _
                                            "Codigo_Maquina", Codigo_Maquina, _
                                            "Descri_Maquina", Descri_Maquina}
            Try
                mstrError = ""
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                pDT = Conexion.ObtenerDataTable("usp_NM_MaquinaPreTejido_Listar_2", objParametro)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
    End Class
End Namespace
