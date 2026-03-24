Public Class Form1
    Private Sub Img_foto_Click(sender As Object, e As EventArgs) Handles img_foto.Click
        Try
            With OpenFileDialog1
                .Title = "Selecione uma Foto"
                .InitialDirectory = Application.StartupPath & "\Fotos"
                .ShowDialog()
                diretorio = .FileName
                img_foto.Load(diretorio)
            End With
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Conecta_banco()
        Carregar_dados()
        Carregar_tipo()

    End Sub

    Private Sub Btn_gravar_Click(sender As Object, e As EventArgs) Handles btn_gravar.Click

        Try

            sql = "select * from venda_carros where DescricaoProduto='" & txt_desc.Text & "'"
            rs = db.Execute(sql)

            If rs.EOF = True Then

                sql = "insert into venda_carros(DescricaoProduto,DataLote,Categoria,MarcaFabricante,QuantidadeDisponivel,PrecoCusto,PrecoVenda,FotoProduto) " &
                  "values('" & txt_desc.Text & "','" & cmb_data_lote.Value & "','" &
                  cmb_categoria.Text & "','" & cmb_marca.Text & "','" &
                  qtd_disponivel.Value & "','" & text_custo.Text & "','" &
                  cmb_venda.Text & "','" & diretorio & "')"

                db.Execute(sql)

                MsgBox("Produto cadastrado com sucesso!", MsgBoxStyle.Information, "AVISO")

                Limpar_cadastro()
                Carregar_dados()

            Else

                MsgBox("Produto já cadastrado!", MsgBoxStyle.Exclamation, "AVISO")

            End If

        Catch ex As Exception

            MsgBox("Erro ao gravar: " & ex.Message, MsgBoxStyle.Critical)

        End Try

    End Sub

    Private Sub txt_desc_LostFocus(sender As Object, e As EventArgs)

        Try

            sql = "select * from venda_carros where DescricaoProduto='" & txt_desc.Text & "'"
            rs = db.Execute(sql)

            If Not rs.EOF Then

                txt_desc.Text = rs.Fields("DescricaoProduto").Value
                cmb_data_lote.Value = rs.Fields("DataLote").Value
                cmb_categoria.Text = rs.Fields("Categoria").Value
                cmb_marca.Text = rs.Fields("MarcaFabricante").Value
                qtd_disponivel.Value = rs.Fields("QuantidadeDisponivel").Value
                text_custo.Text = rs.Fields("PrecoCusto").Value
                cmb_venda.Text = rs.Fields("PrecoVenda").Value
                img_foto.Load(rs.Fields("FotoProduto").Value)

            End If

        Catch ex As Exception

            MsgBox("Erro ao carregar produto", MsgBoxStyle.Critical)

        End Try

    End Sub

    Private Sub txt_cpf_DoubleClick(sender As Object, e As EventArgs)
        Limpar_cadastro()
    End Sub

    Private Sub txt_buscar_TextChanged(sender As Object, e As EventArgs) Handles txt_buscar.TextChanged

        Try

            sql = "select * from venda_carros where DescricaoProduto like '%" & txt_buscar.Text & "%'"
            rs = db.Execute(sql)

            With dgv_dados

                cont = 0
                .Rows.Clear()

                Do While Not rs.EOF

                    cont += 1

                    .Rows.Add(cont,
                          rs.Fields("DescricaoProduto").Value,
                          rs.Fields("DataLote").Value,
                          rs.Fields("Categoria").Value,
                          rs.Fields("MarcaFabricante").Value,
                          rs.Fields("QuantidadeDisponivel").Value)

                    rs.MoveNext()

                Loop

            End With

        Catch ex As Exception

            MsgBox("Erro ao buscar dados", MsgBoxStyle.Critical)

        End Try

    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Btn_gravar_Click_1(sender As Object, e As EventArgs) Handles btn_gravar.Click

    End Sub

    Private Sub Dgv_dados_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_dados.CellContentClick

    End Sub
End Class
