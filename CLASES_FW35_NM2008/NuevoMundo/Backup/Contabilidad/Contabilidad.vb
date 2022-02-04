Imports NM.AccesoDatos

Public Class Contabilidad

#Region "-- Variables --"

  Private _strUsuario As String
  Private _intAnno As Int16
  Private _intMes As Int16
  Private _objConexion As AccesoDatosSQLServer

#End Region

#Region "-- Propiedades --"

  Public Property Usuario() As String
    Get
      Return _strUsuario
    End Get
    Set(ByVal Value As String)
      _strUsuario = Value
    End Set
  End Property

  Public Property Anno() As Int16
    Get
      Return _intAnno
    End Get
    Set(ByVal Value As Int16)
      _intAnno = Value
    End Set
  End Property

  Public Property Mes() As Int16
    Get
      Return _intMes
    End Get
    Set(ByVal Value As Int16)
      _intMes = Value
    End Set
  End Property

#End Region

#Region "-- Constructores --"
  Sub New()

  End Sub
#End Region


#Region "-- Metodos --"

  Public Function ProformasProveedores_Corregir() As DataTable
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
      Dim objParametros() As Object = {"NU_ANNO", _intAnno, "NU_MESE", _intMes}
      Return _objConexion.ObtenerDataTable("NM_PROFORMAS_PROVEEDORES", objParametros)
    Catch ex As Exception
      Throw ex
    End Try
  End Function

    Public Function fnc_generarpivotcontatesoproveedores(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByVal pstr_contcuentainicio As String, ByVal pstr_contcuentafinal As String, ByVal pbln_noconsiderar999 As Boolean, ByVal pbln_noconsiderarsaldosiguales As Boolean, ByVal pint_cantmesesxvencer As Int16, ByVal pstr_tesofechafinal As String, ByVal pbln_excepcionarcdr As Boolean) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim lobj_parametros() As Object = {"ptin_contmes", pint_contmes, "pint_contano", pint_contano, "pvch_contcuentainicio", pstr_contcuentainicio, "pstr_contcuentafinal", pstr_contcuentafinal, "ptin_noconsiderar999", IIf(pbln_noconsiderar999, 1, 0), "ptin_noconsiderarsaldosiguales", IIf(pbln_noconsiderarsaldosiguales, 1, 0), "ptin_cantmesesxvencer", pint_cantmesesxvencer, "pvch_tesofechafinal", pstr_tesofechafinal, "ptin_excepcionarcdr", IIf(pbln_excepcionarcdr, 1, 0)}

            _objConexion.EjecutarComando("usp_tes_contatesoproveedores", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_existencias_procesar(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByVal pint_procmesanterior As Int16) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobj_parametros() As Object = {"pint_anno", pint_contano, _
                                                "pint_mes", pint_contmes, _
                                                "ptin_procmesant", pint_procmesanterior}

            _objConexion.EjecutarComando("usp_con_existencias_proceso", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_existencias_listar(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByRef pdtbdatos As DataTable) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lobj_parametros() As Object = {"ptin_tipolista", "1", _
                                                "pint_anno", pint_contano, _
                                                "pint_mes", pint_contmes}

            pdtbdatos = _objConexion.ObtenerDataTable("utb_conta_existencias1_lista", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

#End Region

End Class
