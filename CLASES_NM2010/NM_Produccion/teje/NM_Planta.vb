Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Planta

        Private adescripion As String

        Property descripcion(ByVal codPlanta As String) As String
            Get
                getPlanta(codPlanta)
            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Public Function listar() As DataTable
            Dim DB As New NM_Consulta()
            Return DB.getData("NM_PLanta")
            DB = Nothing
        End Function


        Public Function getTelares() As DataTable

        End Function

        Public Function getPlanta(ByVal pcodigo_planta) As DataTable
            Dim DB As New NM_Consulta()
            Return DB.Query("SELECT * FROM NM_Planta WHERE Codigo_Planta = '" & pcodigo_planta & "'")
            DB = Nothing
        End Function


    End Class
End Namespace