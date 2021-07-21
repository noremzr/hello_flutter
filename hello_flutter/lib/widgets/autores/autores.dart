import 'package:flutter/material.dart';
import 'package:hello_flutter/models/AutorModel.dart';
import 'package:hello_flutter/services/autorService.dart';
import 'package:find_dropdown/find_dropdown.dart';

class MyWidget extends StatefulWidget {
  @override
  _MyWidgetState createState() => _MyWidgetState();
}

class _MyWidgetState extends State<MyWidget> {
  bool loading = true;
  int selectedAutor = 0;
  AutorService autorService = AutorService();
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 2,
      child: Scaffold(
        appBar: AppBar(
          title: Text("Autores"),
          bottom: const TabBar(tabs: <Widget>[
            Tab(
              icon: Icon(Icons.search),
            ),
            Tab(
              icon: Icon(Icons.add_circle_outlined),
            ),
          ]),
        ),
        body: TabBarView(
          children: <Widget>[
            FutureBuilder(
              future: autorService.getAutores(null),
              builder: (BuildContext context,
                  AsyncSnapshot<List<AutorModel>> snapshot) {
                if (snapshot.hasData) {
                  List<AutorModel> users = snapshot.data!;
                  List<DataRow> row = [];
                  for (AutorModel user in users) {
                    user.existe = true;

                    DataRow dRow = DataRow(cells: [
                      DataCell(
                        IconButton(
                          onPressed: () {
                            // goToEditUser(context, user);
                          },
                          splashRadius: 15,
                          icon: const Icon(Icons.edit),
                          color: Colors.blue,
                        ),
                      ),
                      DataCell(
                        IconButton(
                          onPressed: () {
                            // _showMyDialog(context, user);
                          },
                          splashRadius: 15,
                          icon: Icon(Icons.person_remove_sharp),
                          color: Colors.red,
                        ),
                      ),
                      DataCell(
                        Text("${user.codAutor!}"),
                      ),
                      DataCell(
                        Text(user.nome!),
                      )
                    ]);
                    row.add(dRow);
                  }

                  return Padding(
                    padding: EdgeInsets.all(12.0),
                    child: Column(
                      children: [
                        Expanded(
                          child: Column(
                            children: [
                              ConstrainedBox(
                                constraints: new BoxConstraints(
                                    minWidth: 250.0, maxWidth: 300.0),
                                child: FindDropdown<String>(
                                  label: "Autores",
                                  onFind: (String filter) async {
                                    return await getAutoresByName(filter);
                                  },
                                  searchBoxDecoration: InputDecoration(
                                    hintText: "Procurar",
                                    border: OutlineInputBorder(),
                                  ),
                                  onChanged: (String? data) {},
                                ),
                              ),
                              Column(
                                children: [
                                  Container(
                                    child: DataTable(
                                      columns: [
                                        DataColumn(
                                          label: Text("Editar"),
                                        ),
                                        DataColumn(
                                          label: Text("Deletar"),
                                        ),
                                        DataColumn(
                                          label: Text("Cód.Autor"),
                                        ),
                                        DataColumn(
                                          label: Text("Nome.Autor"),
                                        ),
                                      ],
                                      rows: row,
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  );
                }
                return Center(child: CircularProgressIndicator());
              },
            ),
            Center(
              child: Text("Cadastro"),
            ),
          ],
        ),
      ),
    );
  }

  Future<List<String>> getAutoresByName(String name) async {
    AutorFilter autorFilter = new AutorFilter();
    autorFilter.nome = _nameCase(name);
    List<AutorModel> autores = [];
    if (name.length > 0) {
      autores = await autorService.getAutorByName(autorFilter.nome!);
    } else {
      autores = await autorService.getAutores(null);
    }

    List<String> autoresNome = [];
    autores.forEach((element) {
      autoresNome.add(
        element.getDescriptionDropdown(element),
      );
    });
    return autoresNome;
  }

  String _nameCase(String value) {
    if (value.length >= 2) {
      return value.substring(0, 1) + value.substring(1, value.length);
    } else {
      return value;
    }
  }
}
