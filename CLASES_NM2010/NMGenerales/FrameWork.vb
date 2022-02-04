Imports System.Data
Imports NM.AccesoDatos.GeneradorCadenaConexion
Public Class FrameWork
    Public MustInherit Class [Global]
        Dim mstrUsuario As String = ""
        Dim mstrEmpresa As String = ""
        Dim mobjParametros As Parametros = New Parametros
        Dim mdtTabla As DataTable = Nothing
        Dim mdsSet As DataSet = Nothing
        Dim mbooOk As Boolean = True

        Public SP_INSERTAR As String = ""
        Public SP_ACTUALIZAR As String = ""
        Public SP_ELIMINAR As String = ""
        Public SP_REACTIVAR As String = ""
        Public SP_LISTAR As String = ""
        Public SP_BUSCAR As String = ""

        Public Sub SetearProcedures(ByVal pstrModulo As String, ByVal pstrObjeto As String)
            SP_INSERTAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Insertar"
            SP_ACTUALIZAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Actualizar"
            SP_ELIMINAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Eliminar"
            SP_REACTIVAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Reactivar"
            SP_LISTAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Listar"
            SP_BUSCAR = "usp_" + pstrModulo + "_" + pstrObjeto + "_Buscar"
        End Sub

        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property
        Public Property Empresa() As String
            Get
                Empresa = mstrEmpresa
            End Get
            Set(ByVal Value As String)
                mstrEmpresa = Value
            End Set
        End Property
        Public Property Parametros() As Parametros
            Get
                Parametros = mobjParametros
            End Get
            Set(ByVal Value As Parametros)
                mobjParametros = Value
            End Set
        End Property
        Public Property Ok() As Boolean
            Get
                Ok = mbooOk
            End Get
            Set(ByVal Value As Boolean)
                mbooOk = Value
            End Set
        End Property
        Public Property Tabla() As DataTable
            Get
                Tabla = mdtTabla
            End Get
            Set(ByVal Value As DataTable)
                mdtTabla = Value
            End Set
        End Property
        Public Property SetDatos() As DataSet
            Get
                SetDatos = mdsSet
            End Get
            Set(ByVal Value As DataSet)
                mdsSet = Value
            End Set
        End Property
    End Class
    Public Interface IMantenimiento
        Function Insertar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
        Function Actualizar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
        Function Eliminar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
        Function Reactivar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
        Function Listar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
        Function Buscar(ByVal penuBaseDatos As enmBasesDatos) As Boolean
    End Interface
    Public MustInherit Class ICMantenimiento
        Inherits [Global]
        Implements IMantenimiento

        Public Function Actualizar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Actualizar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_ACTUALIZAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_ACTUALIZAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Buscar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Buscar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_BUSCAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_BUSCAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Eliminar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Eliminar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_ELIMINAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_ELIMINAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Insertar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Insertar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_INSERTAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_INSERTAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Listar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Listar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_LISTAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_LISTAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Reactivar(ByVal penuBaseDatos As NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos) As Boolean Implements IMantenimiento.Reactivar
            Dim lstrParametros() As String = Me.Parametros.Arreglo
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(penuBaseDatos)
                If UBound(lstrParametros, 1) > 0 Then
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_REACTIVAR, lstrParametros)
                Else
                    Me.SetDatos = lobjCon.ObtenerDataSet(SP_REACTIVAR)
                End If
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
    End Class
    Public Class Parametros
        Dim mcolParametros As Collection
        Structure stuParametro
            Public strNombre As String
            Public Valor As VariantType
        End Structure

        Sub New()
            mcolParametros = New Collection
        End Sub

        Public Function Agregar(ByVal pstrNombre As String, ByVal pValue As VariantType)
            Dim lstuParametro As stuParametro
            lstuParametro.strNombre = pstrNombre
            lstuParametro.Valor = pValue
            mcolParametros.Add(lstuParametro)
        End Function
        Public Function Limpiar() As Collection
            mcolParametros = New Collection
        End Function
        Public Function Arreglo() As String()
            Dim lstrArreglo() As String
            Dim lstuParametro As stuParametro
            Dim i As Integer

            ReDim lstrArreglo(1)
            For i = 1 To mcolParametros.Count
                lstuParametro = mcolParametros(i)
                ReDim Preserve lstrArreglo(i * 2 - 1)
                lstrArreglo(2 * i - 2) = lstuParametro.strNombre
                lstrArreglo(2 * i - 1) = lstuParametro.Valor
            Next
            Return lstrArreglo
        End Function
    End Class
End Class
