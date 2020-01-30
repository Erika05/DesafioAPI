 update Solicitacao.SolicitacaoItem  
    set DataRealizacao = GETDATE() 
  where Id = '$solicitacaoItemId'