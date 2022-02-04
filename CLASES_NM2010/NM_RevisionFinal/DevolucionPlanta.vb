Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal

    Public Class DevolucionPlanta
        Implements IDisposable


#Region " Declaración de Variables Miembro "
        Private m_strCodigoDevolucion As String
        Private m_dteFechaDevolucion As Date
        Private m_strObservaciones As String
        Private m_dtDetalles As DataTable
        Private m_strUsuarioCreacion As String
        'Private m_dteFechaCreacion As Date
        'Private m_strUsuarioModificacion As String
        'Private m_dteFechaModificacion As Date
#End Region

#Region " Definción de constructores "
        Sub New()
            'm_strCodigoDefecto = String.Empty
            'm_strDescripcionDefecto = String.Empty
            'm_strUsuarioCreacion = String.Empty
            ''m_dteFechaCreacion = Now
            ''m_strUsuarioModificacion = String.Empty
            ''m_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal strCodigoDevolucion As String, _
                ByVal dteFechaDevolucion As Date, _
                ByVal strObservacion As String, _
                ByVal dtDetalles As DataTable)

            m_strCodigoDevolucion = strCodigoDevolucion
            m_dteFechaDevolucion = dteFechaDevolucion
            m_strObservaciones = strObservacion
            m_dtDetalles = dtDetalles

            m_strUsuarioCreacion = String.Empty
            'm_dteFechaCreacion = Now
            'm_strUsuarioModificacion = String.Empty
            'm_dteFechaModificacion = Now
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerDevoluciones(ByVal strCodigoDevolucion As String) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoDevolucionA", strCodigoDevolucion}

            Return adSQL.ObtenerDataTable("UP_ListarDevoluciones", parametros)

        End Function

        Public Overloads Function ObtenerUltimoCodigoDevolucion() As Integer

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim codigoDevolucion As Integer

            codigoDevolucion = CInt(adSQL.ObtenerValor("UP_ObtenerUltimoCodigoDevolucionPlanta"))

            Return codigoDevolucion

        End Function


        Public Sub Insertar()

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

            Dim paramsCabecera As Object() = {"strCodigoDevolucionP", m_strCodigoDevolucion, _
                                            "dteFechaDevolucion", m_dteFechaDevolucion, _
                                            "strObservaciones", m_strObservaciones, _
                                            "strUsuario", m_strUsuarioCreacion}

            Try
                ' Insertar cabecera
                adSQL.EjecutarComando("UP_InsertarDevolucionPlanta", paramsCabecera)


                ' Insertar detalle
                Dim fila As DataRow
                For Each fila In m_dtDetalles.Rows

                    Dim paramsDetalle As Object() = {"strCodigoDevolucionP", m_strCodigoDevolucion, _
                                                    "strCodigoFichaPartida", fila.Item("codigo_ficha"), _
                                                    "strNumeroPieza", fila.Item("codigo_pieza"), _
                                                    "intMetros", fila.Item("metros"), _
                                                    "strCodigoDefecto", fila.Item("codigo_motivo"), _
                                                    "strUsuario", m_strUsuarioCreacion}

                    adSQL.EjecutarComando("UP_InsertarDevolucionPlantaD", paramsDetalle)
                Next

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub Actualizar()

            'Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            'Dim parametros As Object() = {"strCodigoDefecto", m_strCodigoDefecto, _
            '                                "strDescripcionDefecto", m_strDescripcionDefecto, _
            '                                "strUsuario", m_strUsuarioCreacion}

            'adSQL.EjecutarComando("UP_ActualizarMaestroDefecto", parametros)

        End Sub

        'Public Sub Eliminar(ByVal strCodigoDevolucion As String, ByVal strCodigoRollo As String)

        '    Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '    Dim parametros As Object() = {"strCodigoDevolucion", strCodigoDevolucion, _
        '                                    "strCodigoRollo", strCodigoRollo}

        '    adSQL.EjecutarComando("UP_EliminarMaestroDefecto", parametros)

        'End Sub

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region


    End Class

End Namespace