Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    'Clase controladora de la pantalla de COnsumo de insumos Quimicos
    Public Class ControlConsumoIQ
        Public codPartidaEngomado As String
        Public codTEd As String
        Public revTED As Integer
        Public codFase As Integer
        Public codRecetaPret As String
        Public revRecetaPret As String
        Public codRecetatenido As String
        Public revRecetaTenido As Integer
        Public codRecetaEng As String
        Public revRecetaEng As String
        Public codReceta As String
        Public IQsPretRegi As New DataTable()
        Public IQsTenidoRegi As New DataTable()
        Public IQsPrep As New DataTable()
        Public IQsTenido As New DataTable()
        Public IQsEngomado As New DataTable()
        Private objFormulacion As NM_Formulacion
        '1	Preparacion
        '2	Teñido
        '3	Engomado
        '4	Past Encerado


        Public Sub New(ByVal pCodPArtidaEngomado As String)
            codPartidaEngomado = pCodPArtidaEngomado

            'iq.codigo_insumoquimico, iq.descripcion_insumoquimico, riq.be, riq.concentracion
        End Sub

        Public Sub New(ByVal pCodPArtidaEngomado As String, ByVal pcodTed As String, ByVal revTed As String)

            codTEd = pcodTed
            codPartidaEngomado = pCodPArtidaEngomado
            IQsPretRegi.Columns.Add("codigo_insumoquimico", Type.GetType("System.String"))
            IQsPretRegi.Columns.Add("Peso_Pret", Type.GetType("System.Double"))
            IQsTenidoRegi.Columns.Add("codigo_insumoquimico", Type.GetType("System.String"))
            IQsTenidoRegi.Columns.Add("Peso_Tenido", Type.GetType("System.Double"))
        End Sub
        Public Sub RegistrarConsumoPretratamiento(ByVal pLitSodRecicla As Double, ByVal pBeSodRecicla As Double, ByVal litSod50Be As Double)
            Dim fila As DataRow
            Dim objRecetaDetTED As New NM_RecetaDetTED()
            Dim objDatosRecetaTED As New NM_DatosRecetaTED()
            objFormulacion = New NM_Formulacion()
            codFase = 1    ' Preparacion
            'IQSTenido = objFormulacion.
            'IQsTenido = objFormulacion.ListarInsumosQuimicos(codTEd, revTED, codFase)
            '  codRecetaPret = IQsTenido.Rows(0)("codigo_receta")
            objDatosRecetaTED.codPartEngomadoTED = codPartidaEngomado
            objDatosRecetaTED.codReceta = codRecetaPret
            objDatosRecetaTED.revReceta = revRecetaPret
            objDatosRecetaTED.codFase = codFase
            objDatosRecetaTED.litSodaRecicla = pLitSodRecicla
            objDatosRecetaTED.BeSodaRecicla = pBeSodRecicla
            objDatosRecetaTED.litSoda50Be = litSod50Be
            objDatosRecetaTED.fechaCrea = Today
            objDatosRecetaTED.fechaMod = Today
            objDatosRecetaTED.UsuarioCrea = "Devel00"
            objDatosRecetaTED.UsuarioMod = "Devel00"
            objDatosRecetaTED.Insertar()
            For Each fila In IQsPretRegi.Rows
                objRecetaDetTED.codPartidaEngomadoTed = codPartidaEngomado
                objRecetaDetTED.codInsumoQuimico = fila("codigo_insumoquimico")
                objRecetaDetTED.cantidad = fila("Peso_Pret")
                objRecetaDetTED.codReceta = codRecetaPret
                objRecetaDetTED.revReceta = revRecetaPret
                objRecetaDetTED.codFase = codFase
                objRecetaDetTED.fechaCrea = Today
                objRecetaDetTED.fechaMod = Today
                objRecetaDetTED.usuarioCreacion = "devel00"
                objRecetaDetTED.usuarioMod = "devel00"
                objRecetaDetTED.insertar()
            Next

            'Next


        End Sub
        Public Sub RegistrarConsumoTenido(ByVal pLitInicales As Double, ByVal plitPreparados As Double, ByVal plitFinales As Double)
            Dim objDatRecetaTed As New NM_DatosRecetaTED()
            Dim objRecetaDetTED As New NM_RecetaDetTED()
            Dim fila As DataRow
            objDatRecetaTed.codPartEngomadoTED = codPartidaEngomado
            objDatRecetaTed.codReceta = codRecetatenido
            objDatRecetaTed.revReceta = revRecetaTenido
            objDatRecetaTed.codFase = 2 ' teñido
            objDatRecetaTed.litSodaRecicla = 0
            objDatRecetaTed.BeSodaRecicla = 0
            objDatRecetaTed.litSoda50Be = 0
            objRecetaDetTED.fechaCrea = Today
            objRecetaDetTED.fechaMod = Today
            objDatRecetaTed.litInicio = pLitInicales
            objDatRecetaTed.litPreparados = plitPreparados
            objDatRecetaTed.litFinal = plitFinales
            objDatRecetaTed.Insertar()
            For Each fila In IQsTenidoRegi.Rows
                objRecetaDetTED.codPartidaEngomadoTed = codPartidaEngomado
                objRecetaDetTED.codInsumoQuimico = fila("codigo_insumoquimico")
                objRecetaDetTED.cantidad = fila("Peso_Tenido")
                objRecetaDetTED.codReceta = codRecetatenido
                objRecetaDetTED.revReceta = revRecetaTenido
                objRecetaDetTED.codFase = 2
                objRecetaDetTED.fechaCrea = Today
                objRecetaDetTED.fechaMod = Today
                objRecetaDetTED.usuarioCreacion = "devel00"
                objRecetaDetTED.usuarioMod = "devel00"
                objRecetaDetTED.insertar()
            Next
        End Sub
        Public Sub RegistrarConsumoEngomado(ByVal pveces As Integer, ByVal pKg As Double)
            Dim objDatRecetaTed As New NM_DatosRecetaTED()
            Dim objRecetaDetTED As New NM_RecetaDetTED()
            Dim fila As DataRow
            objDatRecetaTed.codPartEngomadoTED = codPartidaEngomado
            objDatRecetaTed.codReceta = codRecetaEng
            objDatRecetaTed.revReceta = revRecetaEng
            objDatRecetaTed.codFase = 3 ' Engomado
            objDatRecetaTed.litSodaRecicla = 0
            objDatRecetaTed.BeSodaRecicla = 0
            objDatRecetaTed.litSoda50Be = 0
            objRecetaDetTED.fechaCrea = Today
            objRecetaDetTED.fechaMod = Today
            Try
                objDatRecetaTed.Insertar()
            Catch ex As Exception
                Throw New Exception("Error al insertar en la tabla NM_DatosRecetaTED:" & ex.Message)
            End Try
            CalcularRecetaEngomado(pveces, pKg)
            For Each fila In IQsEngomado.Rows
                objRecetaDetTED.codPartidaEngomadoTed = codPartidaEngomado
                objRecetaDetTED.codInsumoQuimico = fila("codigo_insumoquimico")
                objRecetaDetTED.cantidad = fila("Cantidad")
                objRecetaDetTED.codReceta = codRecetaEng
                objRecetaDetTED.revReceta = revRecetaEng
                objRecetaDetTED.codFase = 3
                objRecetaDetTED.fechaCrea = Today
                objRecetaDetTED.fechaMod = Today
                objRecetaDetTED.usuarioCreacion = "devel00"
                objRecetaDetTED.usuarioMod = "devel00"
                Try
                    objRecetaDetTED.insertar()
                Catch ex As Exception
                    Throw New Exception(ex.message)
                End Try
            Next
        End Sub
        ' metodo que se encarga de jalar la lista de IQ que seran mostrados en la pagina al iniciarse
        Public Function GetIQPretratamiento() As DataTable
            Dim objformulacion As New NM_Formulacion()
            Dim fila As DataRow
            Dim tabla As New DataTable()
            Try
                IQsPrep = objformulacion.ListarInsumosQuimicos(codTEd, 1, "ENGTED")
                For Each fila In IQsPrep.Rows
                    If Not IsDBNull(fila("codigo_receta")) Then
                        codRecetaPret = Trim((fila("codigo_receta")))
                    Else
                        Throw New Exception("Codigo_Receta sin valor asignado")
                    End If
                    Exit For
                Next
                Return IQsPrep
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetIQTenido() As DataTable
            Dim objformulacion As New NM_Formulacion()
            Dim fila As DataRow
            Dim tabla As New DataTable()
            Try
                IQsTenido = objformulacion.ListarInsumosQuimicos(codTEd, 2, "ENGTED")
                For Each fila In IQsTenido.Rows
                    If Not IsDBNull(fila("codigo_receta")) Then
                        codRecetatenido = Trim((fila("codigo_receta"))) 'fecha_engomado
                    Else
                        Throw New Exception("Codigo_Receta sin valor asignado")
                    End If
                    Exit For
                Next
                Return IQsTenido
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetIQEngomado() As DataTable
            Dim objformulacion As New NM_Formulacion()
            Dim fila As DataRow
            Dim tabla As New DataTable()
            Try
                IQsEngomado = objformulacion.ListarInsumosQuimicos(codTEd, 3, "ENGTED")
                For Each fila In IQsEngomado.Rows
                    If Not IsDBNull(fila("codigo_receta")) Then
                        codRecetaEng = Trim((fila("codigo_receta")))  'fecha_engomado
                    Else
                        Throw New Exception("Codigo_Receta sin valor asignado")
                    End If
                    Exit For
                Next
                Return IQsEngomado
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function CalcularRecetaTenido(ByVal pLitInicio As Double, ByVal pLitPrep As Double, ByVal pLitFinal As Double) As DataTable
            Dim fila As DataRow
            Dim objformulacion As New NM_Formulacion()
            Dim factor As Double = 0
            If pLitInicio >= 0 And pLitPrep >= 0 And pLitFinal >= 0 Then
                factor = pLitInicio + pLitPrep - pLitFinal
                IQsTenido = objformulacion.ListarInsumosQuimicos(codTEd, 2, "ENGTED") 'obtienes todos los insumosquimicos para la fase de teñido    GetIQs(codTEd, revTED, 2)
                If Not IQsTenido.Columns.Contains("Valor") Then ' verifica si existe la columna cantidad
                    IQsTenido.Columns.Add("Valor", Type.GetType("System.Double"))
                End If
                For Each fila In IQsTenido.Rows
                    If Not IsDBNull(fila("Concentracion")) Then
                        fila("Valor") = factor * fila("Concentracion")
                    Else
                        fila("Valor") = 0.0
                    End If
                Next
                Return IQsTenido
            Else
                Return Nothing
            End If
        End Function
        Public Function CalcularRecetaEngomado(ByVal Veces As Integer, ByVal Kg As Double) As DataTable
            Dim fila As DataRow
            Dim factor As Double = 0
            Dim suma As Double = 0
            If Veces >= 0 And Kg >= 0 Then
                factor = Veces * Kg
                IQsEngomado = objFormulacion.ListarInsumosQuimicos(codTEd, 3, "ENGTED") 'Obtiene los insumos quimicos para la fase de engomado  GetIQs(codTEd, revTED, 3)
                If Not IQsEngomado.Columns.Contains("Cantidad") Then ' verifica si existe la columna cantidad
                    IQsEngomado.Columns.Add("Cantidad", Type.GetType("System.Double"))
                End If
                For Each fila In IQsEngomado.Rows
                    If Not IsDBNull(fila("Concentracion")) Then
                        fila("Cantidad") = factor * fila("Concentracion")
                    Else
                        fila("Cantidad") = 0.0
                    End If

                Next
                Return IQsEngomado
            Else
                Return Nothing
            End If
        End Function



    End Class
End Namespace