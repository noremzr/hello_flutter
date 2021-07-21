import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:hello_flutter/services/LocalService.dart';
import 'package:hello_flutter/widgets/usuarios/usuarios.dart';

import 'autores/autores.dart';

class MenuFrm extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(
        title: new Text("Menu"),
        actions: <Widget>[
          IconButton(
            icon: const Icon(Icons.logout),
            tooltip: 'Sair',
            onPressed: () {
              _logOut(context);
            },
          )
        ],
      ),
      body: Padding(
        padding: EdgeInsets.all(24.0),
        child: Row(children: [
          Column(children: [
            Card(
              clipBehavior: Clip.antiAlias,
              child: InkWell(
                splashColor: Colors.blue.withAlpha(30),
                onTap: () => _goToUsers(context),
                child: Icon(
                  Icons.supervised_user_circle_sharp,
                  size: 120.0,
                ),
              ),
            ),
            Text("Usuários"),
          ]),
          Column(
            children: [
              Card(
                clipBehavior: Clip.antiAlias,
                child: InkWell(
                  splashColor: Colors.blue.withAlpha(30),
                  onTap: () => _goToAutores(context),
                  child: Icon(
                    Icons.library_books,
                    size: 120.0,
                  ),
                ),
              ),
              Text("Autores"),
            ],
          )
        ]),
      ),
    );
  }

  _logOut(BuildContext context) async {
    await LocalService.removeItem(LocalService.userVar);
    Navigator.pushReplacementNamed(context, '/Login');
  }

  _goToAutores(BuildContext context) async {
    Navigator.of(context)
        .push(MaterialPageRoute(builder: (context) => MyWidget()));
  }

  _goToUsers(BuildContext context) async {
    Navigator.of(context)
        .push(MaterialPageRoute(builder: (context) => UsuariosPage()));
  }
}
