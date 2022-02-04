Public Class CXC
    Public Class LineaCredito
        Private mstrParametros() As String

        Sub New()
            LimpiarParametros()
        End Sub

        Public Function AgregarParametro(ByVal pstrNombre As String, ByVal pstrValor As Object) As Boolean
            Try
                If UBound(mstrParametros, 1) = 0 Then
                    ReDim Preserve mstrParametros(1)
                Else
                    ReDim Preserve mstrParametros(UBound(mstrParametros, 1) + 2)
                End If
                mstrParametros(UBound(mstrParametros, 1) - 1) = pstrNombre
                mstrParametros(UBound(mstrParametros, 1)) = pstrValor
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function EditarParametro(ByVal pstrNombre As String, ByVal pstrValor As Object) As Boolean
            Dim i As Integer
            Dim lbooOk As Boolean
            Try
                For i = 0 To UBound(mstrParametros, 1) Step 2
                    If mstrParametros(i) = pstrNombre Then
                        mstrParametros(i + 1) = pstrValor
                        lbooOk = True
                        Exit For
                    End If
                Next i
                Return lbooOk
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Sub LimpiarParametros()
            ReDim mstrParametros(0)
        End Sub

        Public Function ListarClientes() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes1 As DataTable
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                If UBound(mstrParametros, 1) > 0 Then
                    ldtRes1 = lobjCon.ObtenerDataTable("usp_TES_LimiteCredito_ListarClientes", mstrParametros)
                Else
                    ldtRes1 = lobjCon.ObtenerDataTable("usp_TES_LimiteCredito_ListarClientes")
                End If
            Catch ex As Exception
                ldtRes1 = Nothing
            Finally
                lobjCon = Nothing
            End Try
            Return ldtRes1
        End Function
    End Class
End Class
