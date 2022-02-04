<%
	Public Function obtenerNombreMes(sMes)
		if sMes = "01" then
			obtenerNombreMes = "Enero"
		elseif sMes = "02" then
			obtenerNombreMes = "Febrero"
		elseif sMes = "03" then
			obtenerNombreMes = "Marzo"
		elseif sMes = "04" then
			obtenerNombreMes = "Abril"
		elseif sMes = "05" then
			obtenerNombreMes = "Mayo"
		elseif sMes = "06" then
			obtenerNombreMes = "Junio"
		elseif sMes = "07" then
			obtenerNombreMes = "Julio"
		elseif sMes = "08" then
			obtenerNombreMes = "Agosto"
		elseif sMes = "09" then
			obtenerNombreMes = "Setiembre"
		elseif sMes = "10" then
			obtenerNombreMes = "Octubre"
		elseif sMes = "11" then
			obtenerNombreMes = "Noviembre"
		elseif sMes = "12" then
			obtenerNombreMes = "Diciembre"
		end if
	End Function
	
	Function FormatDate(iFormat, sFecha)
		if IsDate(sFecha) then
		
		Dim sDia, sMes, sAnio
		
		sDia = Right("0" & DatePart("d", sFecha), 2)
		sMes = Right("0" & DatePart("m", sFecha), 2)
		sAnio = DatePart("yyyy", sFecha)
		
		'dd/mm/yyyy
		if iFormat = 1 then
			sFecha = sDia & "/" & sMes & "/" & sAnio
		'mm/dd/yyyy
		elseif iFormat = 2 then
			sFecha = sMes & "/" & sDia & "/" & sAnio
		'yyyy/mm/dd/
		elseif iFormat = 3 then
			sFecha = sAnio & "/" & sMes & "/" & sDia
		end if
		
		end if
		FormatDate = sFecha
	End Function
%>
