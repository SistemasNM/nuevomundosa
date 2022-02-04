Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia
    Public Class NM_InventarioCanillasReserva

        Public Codigo_Inventario As String
        Public Codigo_Materia_prima As String
        Public Ne_Real As Double
        Public Codigo_Procedencia As String
        Public Cantidad As Integer
        Public Usuario As String
        Public pesocanilla As Double
        Private objUtil As New NM_General.Util

        Sub New()
            Codigo_Inventario = ""
            Codigo_Materia_prima = ""
            Ne_Real = 0
            Codigo_Procedencia = ""
            Cantidad = 0
            pesocanilla = 0
        End Sub

        Function Add() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "ne_real", Ne_Real, _
                                                  "codigo_procedencia", Codigo_Procedencia, _
                                                  "codigo_materia_prima", Codigo_Materia_prima, _
                                                  "cantidad", Cantidad, _
                                                  "Usuario", Usuario, _
                                                  "pesocanilla", pesocanilla, _
                                                  "int_accion", 1}

                lobjConexion.EjecutarComando("usp_hil_InvCanillaReserva_actualizar", lobjParametros)
                Add = True
            Catch
                Add = False
            End Try
        End Function

        Function Update() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "ne_real", Ne_Real, _
                                                  "codigo_procedencia", Codigo_Procedencia, _
                                                  "codigo_materia_prima", Codigo_Materia_prima, _
                                                  "cantidad", Cantidad, _
                                                  "Usuario", Usuario, _
                                                  "pesocanilla", pesocanilla, _
                                                  "int_accion", 2}

                lobjConexion.EjecutarComando("usp_hil_InvCanillaReserva_actualizar", lobjParametros)
                Update = True
            Catch
                Update = False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal nNeReal As Double, ByVal sCodigoProcedencia As String, ByVal pCodMateriaPrima As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "ne_real", nNeReal, _
                                                  "codigo_procedencia", sCodigoProcedencia, _
                                                  "codigo_materia_prima", pCodMateriaPrima}

                lobjConexion.EjecutarComando("usp_hil_InvCanillaReserva_eliminar", lobjParametros)
                Delete = True
            Catch
                Delete = False
            End Try


        End Function

        Function List() As DataTable
            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", ""}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvCanillaReserva_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function

        Function List(ByVal Fecha As Date) As DataTable
            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", Format(Fecha, "yyyy/MM/dd")}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvCanillaReserva_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function

        Function Exist(ByVal sCodigoInventario As String, ByVal nNeReal As Double, ByVal sCodigoProcedencia As String, ByVal pCodMateriaPrima As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "ne_real", nNeReal, _
                                                  "codigo_procedencia", sCodigoProcedencia, _
                                                  "codigo_materia_prima", pCodMateriaPrima}

                dt = lobjConexion.ObtenerDataTable("usp_hil_InvCanillaReserva_Obtener", lobjParametros)

                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function


        Sub Seek(ByVal sCodigoInventario As String, ByVal nNeReal As Double, ByVal sCodigoProcedencia As String, ByVal pCodMateriaPrima As String)

            Dim fila As DataRow

            Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim dt As New DataTable

            Try

                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "ne_real", nNeReal, _
                                                  "codigo_procedencia", sCodigoProcedencia, _
                                                  "codigo_materia_prima", pCodMateriaPrima}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvCanillaReserva_Obtener", lobjParametros)

                For Each fila In dt.Rows
                    Me.Cantidad = fila("cantidad")
                    Me.Codigo_Inventario = fila("codigo_inventario")
                    Me.Codigo_Procedencia = fila("codigo_procedencia")
                    If IsDBNull(fila("Codigo_Materia_prima")) = False Then Codigo_Materia_prima = fila("Codigo_Materia_prima")
                    Me.Ne_Real = fila("ne_real")
                    If IsDBNull(fila("pesocanilla")) = False Then pesocanilla = fila("pesocanilla")
                Next

            Catch ex As Exception

            End Try

        End Sub

    End Class

End Namespace