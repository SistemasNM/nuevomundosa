Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsMaquina

    '============================== Definicion de variables interna ==============================

    Dim mConexion As AccesoDatosSQLServer

    Dim mstrError As String
    Dim mstrCodigo As String
    Dim mstrNombre As String
    Dim mstrEstado As String
    Dim mstrUsuario As String


    '================================= Definición de constructores ===============================
    Public Sub New()
        Dim mConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Costos)
    End Sub

    '================================= Definición de Propiedades =================================

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property


    Public Property Codigo() As String
        Get
            Codigo = mstrCodigo
        End Get
        Set(ByVal strCad As String)
            mstrCodigo = strCad
        End Set
    End Property


    Public Property Nombre() As String
        Get
            Nombre = mstrNombre
        End Get
        Set(ByVal strCad As String)
            mstrNombre = strCad
        End Set
    End Property

    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(ByVal strCad As String)
            mstrEstado = strCad
        End Set
    End Property


    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property



    '=================================== Definicion de metodos  ==================================


    '======== Metodos de Consulta ========

    Public Function Listar() As DataTable
        '************************************************  
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      retorna un listado de las maquinas
        '************************************************  


        Try

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Function

    Public Sub Obtener()
        '************************************************  
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Obtiene los datos de un a maquina
        '************************************************  

        Try
            mstrCodigo = ""
            mstrNombre = ""
            mstrEstado = ""
            mstrUsuario = ""

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub


    '===== Metodos de Actualizacion  =====

    Public Sub Insertar()
        '*****************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite registrar una nueva maquina
        '*****************************************************  

        Try

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub

    Public Sub Modificar()
        '*****************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite modificar datos de la maquina
        '*****************************************************  

        Try
        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub

    Public Sub Eliminar()
        '*****************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente una maquina
        '*****************************************************  

        Try

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        mConexion = Nothing
        MyBase.Finalize()
    End Sub

End Class
