import 'package:flutter/material.dart';
import 'package:hello_flutter/models/UserModel.dart';
import 'package:hello_flutter/services/userService.dart';

class EditarUsuarios extends StatefulWidget {
  final UserModel usuario;
  final UserService userService;

  const EditarUsuarios({
    required this.usuario,
    required this.userService,
  });

  @override
  MeuFormEditado createState() => MeuFormEditado();
}

class MeuFormEditado extends State<EditarUsuarios> {
  final _formKey = GlobalKey<FormState>();

  String retornaExistente() {
    if (widget.usuario.existe!) {
      return "Edição de Usuários";
    } else {
      return "Cadastro de Usuários";
    }
  }

  @override
  Widget build(BuildContext context) {
    // Build a Form widget using the _formKey created above.

    return Scaffold(
        appBar: AppBar(
          title: Text(widget.usuario.usuario!),
        ),
        body: Container(
            child: Builder(
                builder: (context) => Form(
                      key: _formKey,
                      child: Padding(
                        padding: const EdgeInsets.all(12.0),
                        child: Column(
                          children: <Widget>[
                            Text(
                              retornaExistente(),
                              textAlign: TextAlign.center,
                              style: TextStyle(fontSize: 22),
                            ),
                            TextFormField(
                              decoration: InputDecoration(labelText: 'Usuário'),
                              initialValue: widget.usuario.usuario,
                              enabled: !widget.usuario.existe!,
                              // The validator receives the text that the user has entered.
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return 'Informe o Usuário';
                                }
                                return null;
                              },
                              onChanged: (value) =>
                                  widget.usuario.usuario = value,
                              onSaved: (val) =>
                                  setState(() => widget.usuario.usuario),
                            ),
                            TextFormField(
                              decoration: InputDecoration(labelText: 'Senha'),
                              obscureText: true,
                              autocorrect: false,
                              enableSuggestions: false,
                              initialValue: widget.usuario.senha,
                              enabled: true,
                              // The validator receives the text that the user has entered.
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return 'Informe a nova senha';
                                }
                                return null;
                              },
                              onChanged: (value) =>
                                  widget.usuario.senha = value,
                              onSaved: (val) =>
                                  setState(() => widget.usuario.senha),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(12.0),
                              child: ElevatedButton(
                                onPressed: () {
                                  final form = _formKey.currentState;
                                  if (form!.validate()) {
                                    form.save();
                                    this
                                        .widget
                                        .userService
                                        .saveUser(widget.usuario)
                                        .then((value) => {
                                              _showDialog(context),
                                              Navigator.pop(context)
                                            });
                                    // _user.save();

                                  }
                                },
                                child: Text('Salvar'),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ))));
  }

  _showDialog(BuildContext context) {
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text('Salvando!')));
  }
}
