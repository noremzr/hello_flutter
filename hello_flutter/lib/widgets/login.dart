import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hello_flutter/models/UserModel.dart';
import 'package:hello_flutter/services/LocalService.dart';
import 'package:hello_flutter/services/userService.dart';

class Login extends StatefulWidget {
  @override
  LoginFrm createState() => LoginFrm();
}

class LoginFrm extends State<Login> {
  final _formKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    verifyIsLoged();
  }

  bool senhaErrada = false;
  bool usuarioNaoExiste = false;
  final UserModel usuario =
      new UserModel(usuario: '', senha: '', codDocumento: '', existe: false);
  final UserService userService = UserService();
  @override
  Widget build(BuildContext context) {
    // Build a Form widget using the _formKey created above.

    return Scaffold(
      appBar: AppBar(
        title: Text("Login"),
        backgroundColor: Colors.black,
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
                    "Login",
                    textAlign: TextAlign.center,
                    style: TextStyle(fontSize: 22),
                  ),
                  TextFormField(
                    decoration: InputDecoration(labelText: 'Usuário'),
                    initialValue: "",
                    enabled: true,
                    // The validator receives the text that the user has entered.
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Informe o Usuário';
                      }
                      return null;
                    },
                    onChanged: (value) => this.usuario.usuario = value,
                    onSaved: (val) => setState(() => this.usuario.usuario),
                  ),
                  TextFormField(
                    decoration: InputDecoration(labelText: 'Senha'),
                    obscureText: true,
                    autocorrect: false,
                    enableSuggestions: false,
                    initialValue: this.usuario.senha,
                    enabled: true,
                    // The validator receives the text that the user has entered.
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Informe a nova senha';
                      }
                      return null;
                    },
                    onChanged: (value) => this..usuario.senha = value,
                    onSaved: (val) => setState(() => this..usuario.senha),
                  ),
                  Padding(
                    padding: const EdgeInsets.all(12.0),
                    child: Visibility(
                      child: Text(
                          _defineTextErrorLogin(
                            this.senhaErrada,
                            this.usuarioNaoExiste,
                          ),
                          style: TextStyle(color: Colors.red)),
                      maintainSize: true,
                      maintainAnimation: true,
                      maintainState: true,
                      visible: senhaErrada || usuarioNaoExiste,
                    ),
                  ),
                  Padding(
                    padding: const EdgeInsets.all(12.0),
                    child: ElevatedButton(
                      onPressed: () {
                        final form = _formKey.currentState;
                        if (form!.validate()) {
                          form.save();
                          this
                              .userService
                              .validateUser(this.usuario)
                              .then((value) {
                            if (value.senhaDiferente!) {
                              setState(() {
                                senhaErrada = true;
                                usuarioNaoExiste = false;
                              });
                            } else if (value.usuarioNaoExiste!) {
                              setState(() {
                                usuarioNaoExiste = true;
                                senhaErrada = false;
                              });
                            } else {
                              senhaErrada = false;
                              usuarioNaoExiste = false;
                              _setUserLocalStorageSerivce(
                                this.usuario.usuario!,
                              );
                              _goToMenu();
                            }
                          });
                          _showDialog(context);
                        }
                      },
                      child: Text('Entrar'),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  verifyIsLoged() async {
    try {
      final UserModel user = UserModel.fromJson(
        await LocalService.getLocalItem(LocalService.userVar),
      );
      if (user.codDocumento != null) {
        _goToMenu();
      }
    } catch (Exception) {
      print("Não há usuário salvo no local storage!");
    }
  }

  _setUserLocalStorageSerivce(String user) async {
    await userService.getUser(this.usuario.usuario!).then((value) => {
          LocalService.setLocalItem(LocalService.userVar, jsonEncode(value)),
        });
  }

  _goToMenu() {
    Navigator.pushNamedAndRemoveUntil(
      context,
      '/Menu',
      (route) => false,
    );
  }

  _defineTextErrorLogin(bool senhaDiferente, bool usuarioNaoExiste) {
    String retorno = "";

    if (senhaDiferente) {
      retorno = "Senha Incorreta para o usuário";
    } else if (usuarioNaoExiste) {
      retorno = "Usuário não está cadastrado";
    }
    return retorno;
  }

  _showDialog(BuildContext context) {
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text('Carregando!')));
  }
}
