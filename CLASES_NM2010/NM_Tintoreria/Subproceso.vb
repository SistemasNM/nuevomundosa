Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos


Namespace NM.Tintoreria
    Public Class Subproceso
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private _strCodigoSubProceso As String
        Private _strNombreSubProceso As String
        Private _strCodigoEtapa As String
        Private _intRevision As Int16
        Private _strUsuario As String
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property CodigoSubProceso() As String
            Get
                Return _strCodigoSubProceso
            End Get
            Set(ByVal Value As String)
                _strCodigoSubProceso = Value
            End Set
        End Property

        Public Property NombreSubProceso() As String
            Get
                Return _strNombreSubProceso
            End Get
            Set(ByVal Value As String)
                _strNombreSubProceso = Value
            End Set
        End Property

        Public Property CodigoEtapa() As String
            Get
                Return _strCodigoEtapa
            End Get
            Set(ByVal Value As String)
                _strCodigoEtapa = Value
            End Set
        End Property

        Public Property Revision() As Int16
            Get
                Return _intRevision
            End Get
            Set(ByVal Value As Int16)
                _intRevision = Value
            End Set
        End Property

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
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function ObtenerEtapas() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Etapa_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function GetRevision(ByVal pCodigoSubproceso As String) As Integer
            Dim Revision As Integer
            Try
                Dim objParametros As Object() = {"codigo_subproceso", pCodigoSubproceso}
                Revision = CInt(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Subproceso_GetRevision", objParametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return Revision
        End Function

    Public Function ListarCabecera(ByVal pintTipoConsulta As Integer, ByVal pstrEtapa As String) As DataTable
      Dim dtblDatos As DataTable
      Try
        Dim objParametros As Object() = { _
        "ptin_tipoconsulta", pintTipoConsulta, _
        "pvch_codigoetapa", pstrEtapa _
        }

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_SubprocesoPorEtapa_Select", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarProceso_Etapa(ByVal pDescEtapa As String) As DataTable
      Dim dtblDatos As New DataTable
      Try
        Dim objParametros As Object() = {"descripcion_etapa", pDescEtapa}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_SubprocesoPorDescEtapa_Select", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarCodDesc() As DataTable
      Dim dtblDatos As DataTable

      Try

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_CodDesc_Select")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Obtener(ByVal pCodigo As String) As DataTable
      Dim dtblDatos As DataTable
      Dim objParametros() As Object = {"codigo_subproceso", pCodigo}
      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_Subproceso_OBTENER", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Listar() As DataTable
      Dim dtblDatos As DataTable

      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_Select")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarSubprocesos_por_Receta(ByVal pCodigoReceta As String) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros As Object() = {"codigo_receta", pCodigoReceta}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_Select_ByReceta", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarSubprocesos_por_Maquina(ByVal pCodigoMaquina As String) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros As Object() = {"codigo_maquina", pCodigoMaquina}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_Select_ByMaquina", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function AgregarCabecera(ByVal pCodigo As String, ByVal pDescripcion As String, ByVal pRevisionSubproceso As Integer, ByVal pEtapa As String, ByVal pUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pCodigo, "descripcion_subproceso", pDescripcion, "revision_subproceso", pRevisionSubproceso, "codigo_etapa", pEtapa, "usuario_creacion", pUsuario}
        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Insert", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function AgregarDetalle(ByVal pCodigo As String, ByVal pSeccion As String, ByVal pRevisionSubproceso As Integer, ByVal pMaquina As String, ByVal pRevisionMaquina As String, ByVal pOperacion As String, ByVal pVelocidad As String, ByVal pPases As String, ByVal pReceta As String, ByVal pRevisionReceta As String, ByVal pUsuario As String, ByVal intEstado As Integer) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pCodigo, "seccion", pSeccion, "revision_subproceso", pRevisionSubproceso, "codigo_maquina", pMaquina, "revision_maquina", pRevisionMaquina, "codigo_operacion", pOperacion, "velocidad", pVelocidad, "Pases", pPases, "codigo_receta", pReceta, "revision_receta", pRevisionReceta, "usuario_creacion", pUsuario, "ESTADO", intEstado}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Detalle_Insert", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function ModificarDetalle(ByVal pCodigo As String, ByVal pSeccion As String, ByVal pMaquina As String, ByVal pOperacion As String, ByVal pVelocidad As String, ByVal pPases As String, ByVal pReceta As String, ByVal pUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pCodigo, "seccion", pSeccion, "codigo_maquina", pMaquina, "codigo_operacion", pOperacion, "velocidad", pVelocidad, "Pases", pPases, "codigo_receta", pReceta, "usuario_modificacion", pUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Detalle_Update", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function ModificarRevision(ByVal pCodigo As String, ByVal pRevisionSubproceso As Integer) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pCodigo, "revision_subproceso", pRevisionSubproceso}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Detalle_Revision", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function EliminarDetalle(ByVal pCodigo As String, ByVal pSeccion As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pCodigo, "seccion", pSeccion}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Detalle_Delete", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function AgregarDetalleXML(ByVal dtDatos As DataTable, ByVal pSubproceso As String, ByVal pRevisionSubproceso As String) As Boolean
      Dim bResultado As Boolean = False
      Dim lXml As String
      Dim dRowDetalle As DataRow
      For Each dRowDetalle In dtDatos.Rows
        dRowDetalle("codigo_subproceso") = pSubproceso
      Next

      lXml = GeneraXml(dtDatos)
      Try
        Dim objParametros As Object() = {"xml", lXml, "codigo_subproceso", pSubproceso, "revision_subproceso", pRevisionSubproceso}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Subproceso_Detalle_InsertXML", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function ProcesarSubProcesoXML(ByVal dtDatos As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Dim lXml As String
      Dim dRowDetalle As DataRow
      'For Each dRowDetalle In dtDatos.Rows
      '    dRowDetalle("codigo_subproceso") = pSubproceso
      'Next

      lXml = GeneraXml(dtDatos)
      Try
        Dim objParametros As Object() = {"p_XMLData", lXml, "p_var_CodigoSubProceso", _strCodigoSubProceso, _
        "p_int_RevisionSubProceso", _intRevision, "p_var_Descripcion", _strNombreSubProceso, _
        "p_var_CodigoEtapa", Me._strCodigoEtapa, "p_var_Usuario", _strUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("usp_TIN_SubprocesoXML_Procesar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function VerificarSubProcesoXML(ByVal dtDatos As DataTable) As DataTable
      Dim lXml As String
      lXml = GeneraXml(dtDatos)
      Try
        Dim objParametros As Object() = {"p_XMLData", lXml, "p_var_CodigoSubProceso", _strCodigoSubProceso, "p_var_Usuario", _strUsuario}
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_VerificarSubProcesoArticulos_Obtener", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ListarDetalle(ByVal pstrSubproceso As String) As DataTable
      Dim dtblDatos As DataTable

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pstrSubproceso}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_Detalle", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ObtenerDescripcionReceta(ByVal pstrReceta As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_receta", pstrReceta}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Receta_SelectId", objParametros)
        If Not dtblDatos.Rows.Count.Equals(0) Then
          strResultado = dtblDatos.Rows(0)("descripcion_receta").ToString
        Else
          strResultado = String.Empty
        End If

      Catch ex As Exception
        Throw ex
      End Try

      Return strResultado
    End Function

    Public Function ObtenerAutogenerado(ByVal pSubproceso As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_subproceso", pSubproceso}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_SelectId", objParametros)
        If Not dtblDatos.Rows.Count.Equals(0) Then
          strResultado = (CType(dtblDatos.Rows(0)("revision_subproceso"), Integer) + 1).ToString
        Else
          strResultado = "1"
        End If

      Catch ex As Exception
        Throw ex
      End Try

      Return strResultado
    End Function

    Public Function ActualizacionAutomatica_Rutas(ByVal pCodigo As String, ByVal strUsuario As String) As Boolean
      Try
        Dim objParametros As Object() = {"SUBPROCESO", pCodigo, "var_Usuario", strUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("SP_NM_ACTUALIZA_RUTA_SUBPROCESO", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ActualizacionAutomatica_Maestros_Insert(ByVal pCodigo As String, ByVal pNombreMaestro As String, ByVal pUsuariocreacion As String) As Boolean
      Try
        Dim objParametros As Object() = {"codigo", pCodigo, "nombre_maestro", pNombreMaestro, "usuario_creacion", pUsuariocreacion}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_ActualizacionAutomatica_Maestros_Insert", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Sub ActulizarValoresPickup_By_Subproceso(ByVal pRevisionSubproceso As Integer, ByVal pdtDetalle As DataTable)
      Try
        Dim xml As String
        pdtDetalle.TableName = "Pickup"
        xml = GeneraXml(pdtDetalle)
        Dim objParametros As Object() = {"revision_subproceso", pRevisionSubproceso, "xml", xml}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_ActualizarValoresPickup_By_Subproceso", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Sub
        '------------------------------------------------------------------------------------
        'DG - REQSIS201700001 - 27/10/2017 - INI
        '------------------------------------------------------------------------------------
        Public Function ValidaOperacionPorRecetaGeneral(ByVal pCodigoMaquina As String, ByVal pCodigoOperacion As String, ByVal pCodigoReceta As String) As DataTable
            Dim dtbldatos As DataTable
            Try
                Dim objParametros As Object() = {"codigoMaquina", pCodigoMaquina, "codigoOperacion", pCodigoOperacion, "codigoReceta", pCodigoReceta}
                dtbldatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ValidarOperacionPorRecetaGeneral", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function
        '------------------------------------------------------------------------------------
        'DG - REQSIS201700001 - 27/10/2017 - FIN
        '------------------------------------------------------------------------------------
        Function GeneraXml(ByVal dtDatos As DataTable) As String
            Dim xmlDOM As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            Dim objXML As New NM_General.Util
            With xmlDOM
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtDatos.Rows.Count - 1
                    nodo = .CreateElement(dtDatos.TableName)
                    For j As Integer = 0 To dtDatos.Columns.Count - 1
                        If Not IsDBNull(dtDatos.Rows(i)(j)) Then
                            nodoChild = .CreateElement(dtDatos.Columns(j).ColumnName)
                            nodoChild.InnerText = Trim(CType(dtDatos.Rows(i)(j), String))
                            nodo.AppendChild(nodoChild)
                        End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return objXML.EncodeXML(.OuterXml())
            End With
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace