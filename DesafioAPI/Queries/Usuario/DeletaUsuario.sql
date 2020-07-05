DELETE FROM mantis_user_table
WHERE username ='$nomeUsuario' OR email='$email';
DELETE FROM mantis_email_table
WHERE email='$email';