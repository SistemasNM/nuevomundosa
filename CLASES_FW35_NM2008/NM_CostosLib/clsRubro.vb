Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsRubro

#Region "-- Variables --"

    Protected Friend mConexion As AccesoDatosSQLServer
    Protected Friend mstrError As String
    Protected Friend mstrEmpresa As String = ""
    Protected Friend mintCodigo As Integer = 0
    Protected Friend mstrDescripcionCorta As String = ""
    Protected Friend mstrDescripcionLarga As String = ""
    Protected Friend mstrTipoRubro As String = ""
    Protected Friend mstrEstado As String = ""
    Protected Friend mstrUsuario As String = ""

#End Region

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================

#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property Codigo() As Integer
        Get
            Codigo = mintCodigo
        End Get
        Set(ByVal intVal As Integer)
            mintCodigo = intVal
        End Set
    End Property

    Public Property DescripcionCorta() As String
        Get
            DescripcionCorta = mstrDescripcionCorta
        End Get
        Set(ByVal strCad As String)
            mstrDescripcionCorta = strCad
        End Set
    End Property

    Public Property DescripcionLarga() As String
        Get
            DescripcionLarga = mstrDescripcionLarga
        End Get
        Set(ByVal strCad As String)
            mstrDescripcionLarga = strCad
        End Set
    End Property

    Public Property TipoRubro() As String
        Get
            TipoRubro = mstrTipoRubro
        End Get
        Set(ByVal strCad As String)
            mstrTipoRubro = strCad
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

#End Region

    '=================================== Definicion de metodos  ==================================


    '======== Metodos de Consulta ========

    

End Class
