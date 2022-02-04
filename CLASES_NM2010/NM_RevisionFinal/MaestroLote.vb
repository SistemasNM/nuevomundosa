Option Strict On

Imports NM.AccesoDatos
'Imports NM_General
'Imports System.Text

Namespace NM.RevisionFinal
    Public Class MaestroLote
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoArticulo As String
        Private m_intRevisionArticulo As Integer
        Private m_strCodigoLote As String
        Private m_strNumeroLote As String
        Private m_strUsuarioCreacion As String
        Private m_dteFechaCreacion As Date
        Private m_strUsuarioModificacion As String
        Private m_dteFechaModificacion As Date
        Private m_intclaselote As Int16
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strCodigoArticulo = String.Empty
            m_intRevisionArticulo = 0
            m_strCodigoLote = String.Empty
            m_strNumeroLote = String.Empty
            m_strUsuarioCreacion = String.Empty
            m_dteFechaCreacion = Now
            m_strUsuarioModificacion = String.Empty
            m_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal strCodigoLote As String, ByVal strNumeroLote As String)
            m_strCodigoLote = strCodigoLote
            m_strNumeroLote = strNumeroLote

            m_strCodigoArticulo = String.Empty
            m_intRevisionArticulo = 0
            m_strUsuarioCreacion = String.Empty
            m_dteFechaCreacion = Now
            m_strUsuarioModificacion = String.Empty
            m_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal strCodigoLote As String, _
                ByVal strNumeroLote As String, _
                ByVal strCodigoArticulo As String, _
                ByVal pintClaseLote As Int16)

            m_strCodigoLote = strCodigoLote
            m_strNumeroLote = strNumeroLote
            m_strCodigoArticulo = strCodigoArticulo
            m_intclaselote = pintClaseLote

            m_intRevisionArticulo = 0
            m_strUsuarioCreacion = String.Empty
            m_dteFechaCreacion = Now
            m_strUsuarioModificacion = String.Empty
            m_dteFechaModificacion = Now
            m_intclaselote = pintClaseLote
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerLote(ByVal strCodigoLote As String) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoLote", strCodigoLote}

            Return adSQL.ObtenerDataTable("UP_ObtenerLote", parametros)

        End Function

        Public Overloads Function ObtenerNumLotePorArticulo(ByVal strCodigoLote As String, ByVal strCodigoArticulo As String, ByVal pintVariablearticulo As Int16) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_CodigoLote", strCodigoLote, "pvch_CodigoArticulo", strCodigoArticulo, "ptin_variablearticulo", pintVariablearticulo}

            Return adSQL.ObtenerDataTable("UP_ObtenerNumLotePorArticulo", parametros)

        End Function

        Public Overloads Function ObtenerLotes() As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

            Return adSQL.ObtenerDataTable("UP_ObtenerLotes")

        End Function
        'ACTUALIZADO X ObtenerLotesPorArticuloxClase
        'Public Overloads Function ObtenerLotesPorArticulo(ByVal strCodigoArticulo As String) As DataTable

        '  Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '  Dim parametros As Object() = {"strCodigoArticulo", strCodigoArticulo}

        '  Return adSQL.ObtenerDataTable("UP_ObtenerLotexArticulo", parametros)

        'End Function

        Public Overloads Function ObtenerLotesxArticuloxVariable(ByVal pstrCodigoArticulo As String, ByVal pintVariableArticulo As Int16) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_codigoarticulo", pstrCodigoArticulo, "ptin_variablearticulo", pintVariableArticulo}

            Return adSQL.ObtenerDataTable("UP_ObtenerLotexArticulo", parametros)

        End Function

        'REQSIS201900035 -DG - INI
        'Public Sub Insertar()

        '  Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '  Dim parametros As Object() = {"pvch_CodigoArticulo", m_strCodigoArticulo, _
        '                                  "pint_RevisionArticulo", m_intRevisionArticulo, _
        '                                  "pvch_CodigoLote", m_strCodigoLote, _
        '                                  "pvch_NumeroLote", m_strNumeroLote, _
        '                                  "pchr_Usuario", m_strUsuarioCreacion, _
        '                                  "ptin_variablearticulo", m_intclaselote}

        '  adSQL.EjecutarComando("UP_InsertarMaestroLote", parametros)

        '    End Sub
        Public Sub Insertar(ByVal strLote As String, ByVal strNumLote As String, ByVal strCodArticulo As String, ByVal intRevArticulo As Integer, ByVal intvariable As Integer, ByVal strUsuario As String)

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_CodigoArticulo", strCodArticulo, _
                                            "pint_RevisionArticulo", intRevArticulo, _
                                            "pvch_CodigoLote", strLote, _
                                            "pvch_NumeroLote", strNumLote, _
                                            "pchr_Usuario", strUsuario, _
                                            "ptin_variablearticulo", intvariable}

            adSQL.EjecutarComando("UP_InsertarMaestroLote", parametros)

        End Sub
        'REQSIS201900035 - DG - FIN

        'REQSIS201900035 -DG - INI
        'Public Sub Actualizar()

        '  Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '  Dim parametros As Object() = {"pvch_CodigoArticulo", m_strCodigoArticulo, _
        '                                  "pint_RevisionArticulo", m_intRevisionArticulo, _
        '                                  "pvch_CodigoLote", m_strCodigoLote, _
        '                                  "pvch_NumeroLote", m_strNumeroLote, _
        '                                  "pchr_Usuario", m_strUsuarioCreacion, _
        '                                  "ptin_variablearticulo", m_intclaselote}

        '  adSQL.EjecutarComando("UP_ActualizarMaestroLote", parametros)

        '    End Sub
        Public Sub Actualizar(ByVal strLote As String, ByVal strNumLote As String, ByVal strCodArticulo As String, ByVal intRevArticulo As Integer, ByVal intvariable As Integer, ByVal strUsuario As String)

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_CodigoArticulo", strCodArticulo, _
                                            "pint_RevisionArticulo", intRevArticulo, _
                                            "pvch_CodigoLote", strLote, _
                                            "pvch_NumeroLote", strNumLote, _
                                            "pchr_Usuario", strUsuario, _
                                            "ptin_variablearticulo", intvariable}

            adSQL.EjecutarComando("UP_ActualizarMaestroLote", parametros)

        End Sub
        Public Function ValidarRollosTransito(ByVal strNumLote As String, ByVal strCodArticulo As String) As Boolean
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_NumeroLote", strNumLote, "pvch_CodArticulo", strCodArticulo}
            Dim dt As DataTable = adSQL.ObtenerDataTable("USP_VALIDAR_ROLLOS_TRANSITO_LOTIZADO", parametros)
            If Integer.Parse(dt.Rows(0).Item("result").ToString) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        'REQSIS201900035 -DG - FIN

        Public Sub Eliminar(ByVal strCodigoArticulo As String, ByVal intRevisionArticulo As Integer, ByVal strCodigoLote As String, ByVal pintVariableArticulo As Int16, ByVal strUsuario As String)

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_CodigoArticulo", strCodigoArticulo, _
                                "pint_RevisionArticulo", 0, _
                                "pvch_CodigoLote", strCodigoLote, _
                                "ptin_variablearticulo", pintVariableArticulo,
                                          "pchr_Usuario", strUsuario}

            adSQL.EjecutarComando("UP_EliminarMaestroLote", parametros)

        End Sub

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region

        Public Overloads Function Obtiene_Lote_Ingresado(ByVal cod As String) As DataTable
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"cod", cod}
            Return adSQL.ObtenerDataTable("UP_OBTIENE_LOTE_INGRESADO", parametros)
        End Function

#Region "LUIS_AJ 20210505"

        Public Function ObtenerNumeroLoteArticulo(ByVal strArticuloCorto As String, ByVal strVariable As String, ByVal strCodigoLote As String) As String
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"pvch_CodArticulo", strArticuloCorto, "pvch_Variable", strVariable, "pvch_CodigoLote", strCodigoLote}

            Return CStr(adSQL.ObtenerValor("USP_RVF_OBTENER_NUMERO_LOTE", parametros))

        End Function



#End Region
    End Class

End Namespace