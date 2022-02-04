Option Strict Off

'Imports System.Data
Imports NM.AccesoDatos
Imports NUEVOMUNDO.Tintoreria.NM.Tintoreria

Namespace NM.Tintoreria
    Public Class ProgramaTintoreria
        Implements IDisposable
        '        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer

#Region " Declaracion de Variables Miembro "

        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
        Private proceso As String
        Private operacion As String
        Private articulo As String

#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub

        Sub New(ByVal p As String, ByVal o As String, ByVal a As String)
            If p = "" Then
                p = "%"
            End If
            If o = "" Then
                o = "%"
            End If
            If a = "" Then
                a = "%"
            End If
            proceso = p
            operacion = o
            articulo = a
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "
        Public Function Listar(ByVal strProceso As String, ByVal strOperacion As String, _
        ByVal strArticulo As String) As DataTable

            Dim dtblDatos As New DataTable
            proceso = strProceso
            operacion = strOperacion
            articulo = strArticulo
            If Trim(proceso) = "" Then
                proceso = "%"
            End If
            If Trim(operacion) = "" Then
                operacion = "%"
            End If
            If Trim(articulo) = "" Then
                articulo = "%"
            End If
            '***
            Dim objParametros As Object() = {"proceso", proceso, "operacion", operacion, "articulo", articulo}
            dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Programa_Tintoreria", objParametros)
            Return dtblDatos
        End Function

        Public Function Grabar(ByVal dtblProgramaTinto As DataTable, ByVal strUsuario As String) As Integer
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util
                Dim intFilasDevueltas As Integer = 0
                Dim objParametros As Object() = {"xml_DataTable", objXml.GeneraXml(dtblProgramaTinto), "p_Usuario", strUsuario}
                'Dim dtbResultado As DataTable = ObtenerDataTable
                intFilasDevueltas = m_sqlDtAccTintoreria.EjecutarComando("UP_InsertarProgramacionTintoreria", objParametros)

                Return intFilasDevueltas
                'If Not dtbResultado Is Nothing Then
                '    intFilasDevueltas = CStr((dtbResultado.Rows(0).Item(0)))
                'End If
                'Return intFilasDevueltas
                'Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Function ListarProceso() As DataTable
            Dim dtblDatos As New DataTable
            Dim objParametros As Object() = {"codigo_proceso", proceso}
            dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Proceso_OP_SelectId", objParametros)
            Return dtblDatos
        End Function

        Public Function ListarOperacion() As DataTable
            Dim dtblDatos As New DataTable
            Dim objParametros As Object() = {"codigo_tipooperacion", proceso}
            dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_TipoOperacion_OP_SelectId", objParametros)
            Return dtblDatos
        End Function

        Public Function ObtenerProceso() As DataTable
            Try
                'Dim objParametros As Object() = {"CO_CLIE", strCodigoCliente, "NO_CLIE", strNombreCliente}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_TINTO_OBTENER_PROCESOS")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtenerTipoOperacion() As DataTable
            Try
                'Dim objParametros As Object() = {"CO_CLIE", strCodigoCliente, "NO_CLIE", strNombreCliente}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_TINTO_OBTENER_TIPOOPERACION")
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Function ExistProceso(ByVal pCodigo As String) As Boolean
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"codigo", pCodigo}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("SP_TINTO_OBTENER_PROCESOS_P", objParametros)
                Return dtblDatos.Rows.Count > 0

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ExistTipoOperacion(ByVal pCodigo As String) As Boolean
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"codigo", pCodigo}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("SP_TINTO_OBTENER_TIPOOPERACION_P", objParametros)
                Return dtblDatos.Rows.Count > 0

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ExistArticulo(ByVal pCodigo As String) As Boolean
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"codigo", pCodigo}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("SP_TINTO_OBTENER_ARTICULO_P", objParametros)
                Return dtblDatos.Rows.Count > 0

            Catch ex As Exception
                Throw ex
            End Try

        End Function



        Public Function Descripcion(ByVal pstrOperacion As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operacion", pstrOperacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("descripcion_operacion").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function Maquina(ByVal pstrOperacion As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operacion", pstrOperacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("codiqo_maquina").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function Agregar(ByVal pOperacion As String, ByVal pDescOperacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion, "descripcion_operacion", pDescOperacion, "usuario_Creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Insert", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function


        Public Function Modificar(ByVal pOperacion As String, ByVal pDescOperacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion, "descripcion_operacion", pDescOperacion, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Update", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Eliminar(ByVal pOperacion As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Delete", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function EliminarMaquinaRevision(ByVal pCodigoMaquina As String, ByVal pNumeroRevision As Integer)
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_maquina", pCodigoMaquina, "numero_revision", pNumeroRevision}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_DeleteMaquinaRevision", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    Public Function ufn_insumoquimicos_thermopowder() As DataTable
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Try
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_insumosthermopowder_lista")
      Catch ex As Exception
        Throw ex
      End Try
    End Function

  End Class
End Namespace
