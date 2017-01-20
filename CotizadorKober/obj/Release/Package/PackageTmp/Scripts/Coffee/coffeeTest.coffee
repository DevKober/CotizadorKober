root = exports ? this
root.display = -> alert 'Funcion llamada desde coffescript'
#window.onload = display()
#window.location = "/Home/ConsultaCodigo?codigo=" + $("#edtCodigo").val();

root.buscarCodigo = (codigo) -> window.location = "/Home/ConsultaCodigo?codigo=#{codigo}" 

