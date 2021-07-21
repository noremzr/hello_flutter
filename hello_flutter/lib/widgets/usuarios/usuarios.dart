import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hello_flutter/models/UserModel.dart';
import 'package:hello_flutter/services/userService.dart';
import 'package:hello_flutter/widgets/usuarios/editarUsuarios.dart';

class UsuariosPage extends StatefulWidget {
  @override
  UsuariosPageFrm createState() => UsuariosPageFrm();
}

class UsuariosPageFrm extends State<UsuariosPage> {
  final UserService userService = UserService();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Usuários"),
      ),
      body: FutureBuilder(
        future: userService.getUsers(),
        builder:
            (BuildContext context, AsyncSnapshot<List<UserModel>> snapshot) {
          if (snapshot.hasData) {
            List<UserModel> users = snapshot.data!;
            List<DataRow> row = [];
            for (UserModel user in users) {
              user.existe = true;

              DataRow dRow = DataRow(cells: [
                DataCell(
                  IconButton(
                    onPressed: () {
                      goToEditUser(context, user);
                    },
                    splashRadius: 15,
                    icon: const Icon(Icons.edit),
                    color: Colors.blue,
                  ),
                ),
                DataCell(
                  IconButton(
                    onPressed: () {
                      _showMyDialog(context, user);
                    },
                    splashRadius: 15,
                    icon: Icon(Icons.person_remove_sharp),
                    color: Colors.red,
                  ),
                ),
                DataCell(
                  Text(user.usuario!),
                )
              ]);
              row.add(dRow);
            }

            return DataTable(
              columns: [
                DataColumn(label: Text("Editar")),
                DataColumn(label: Text("Deletar")),
                DataColumn(label: Text("Usuario")),
              ],
              rows: row,
            );
          }

          return Center(child: CircularProgressIndicator());
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          UserModel user = new UserModel(
            usuario: '',
            senha: '',
            codDocumento: '',
            existe: false,
          );
          goToEditUser(context, user);
        },
        child: const Icon(Icons.add),
        backgroundColor: Colors.lightGreen,
      ),
    );
  }

  void goToEditUser(BuildContext context, UserModel user) {
    Navigator.of(context)
        .push(MaterialPageRoute(
            builder: (context) => EditarUsuarios(
                  usuario: user,
                  userService: userService,
                )))
        .then((value) => setState(() {}));
  }

  Future<void> _showMyDialog(BuildContext context, UserModel user) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false, // user must tap button!
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Realizando Exclusão'),
          content: SingleChildScrollView(
            child: Column(
              children: <Widget>[
                Text('Confirma a exclusão'),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: Text('Cancelar'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              style: ButtonStyle(alignment: Alignment.centerLeft),
              child: Text('Confirmar'),
              onPressed: () {
                Navigator.of(context).pop();
                userService.deleteUser(user).then((value) => {
                      setState(() => {}),
                    });
                _showDeleteDialog(context);
                // GoToEditUser(context, user);
              },
            ),
          ],
        );
      },
    );
  }

  _showDeleteDialog(BuildContext context) {
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text('Deletado!')));
  }
}
