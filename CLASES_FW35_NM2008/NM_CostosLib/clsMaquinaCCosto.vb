Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsMaquinaCCosto
    Dim mConexion As AccesoDatosSQLServer

    Dim mstrError As String
    Dim mstrCodMaquina As String
    Dim mstrNomMaquina As String

    Dim mstrCodCCosto As String
    Dim mstrNomRecurso As String
    Dim mdblPorcDist As Double

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


    Public Property CodigoMaquina() As String
        Get
            CodigoMaquina = mstrCodMaquina
        End Get
        Set(ByVal strCad As String)
            mstrCodMaquina = strCad
        End Set
    End Property

    Public Property NombreMaquina() As String
        Get
            NombreMaquina = mstrNomMaquina
        End Get
        Set(ByVal strCad As String)
            mstrNomMaquina = strCad
        End Set
    End Property

    Public Property CodigoCCosto() As String
        Get
            CodigoCCosto = mstrCodCCosto
        End Get
        Set(ByVal strCad As String)
            mstrCodCCosto = strCad
        End Set
    End Property

    Public Property NombreCCosto() As String
        Get
            NombreCCosto = mstrNomRecurso
        End Get
        Set(ByVal strCad As String)
            mstrNomRecurso = strCad
        End Set
    End Property

    Public Property PorcentajeDist() As Double
        Get
            PorcentajeDist = mdblPorcDist
        End Get
        Set(ByVal dblNum As Double)
            mdblPorcDist = dblNum
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
        'Proposito :      retorna un listado de Rubro
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
        'Proposito :      Obtiene los datos de un Rubro
        '************************************************  

        Try
            mstrCodMaquina = ""
            mstrNomMaquina = ""
            mstrCodCCosto = ""
            mstrNomRecurso = ""
            mstrEstado = ""
            mstrUsuario = ""
            mdblPorcDist = 0

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub


    '===== Metodos de Actualizacion  =====

    Public Sub Insertar()
        '*****************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite registrar un nuevo Rubro
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
        'Proposito :      Permite modificar datos del Rubro
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
        'Proposito :      Elimina logicamente un Rubro
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
