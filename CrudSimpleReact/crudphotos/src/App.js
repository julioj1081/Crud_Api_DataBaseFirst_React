import { useEffect, useState } from "react";
import { getPhotos,createPhoto,updatePhoto,deletePhoto } from "./services/photoService";

function App() {

    const [photos, setPhotos] = useState([]);

    const [photo, setPhoto] = useState({
        idPhoto: 0,
        nombre: "",
        descripcion: "",
        url: ""
    });

    const cargarFotos = async () => {
        const data = await getPhotos();
        setPhotos(data);
    };

    useEffect(() => {
        cargarFotos();
    }, []);

    function validarFuncion(photo) {
      if (
        photo.nombre.trim() === "" ||
        photo.descripcion.trim() === "" ||
        photo.url.trim() === ""
      ) {
        alert("Todos los campos son obligatorios");
        return false;
      }

      return true;
    }

    const guardar = async () => {
        if (!validarFuncion(photo)) {
          return;
        }

        if (photo.idPhoto === 0) {
          await createPhoto(photo);
        } else {
          await updatePhoto(photo);
        }
        
        setPhoto({
          idPhoto: 0,
          nombre: "",
          descripcion: "",
          url: ""
        });
        
        cargarFotos();
    };

    const editar = (item) => {
        setPhoto(item);
    };

    const eliminar = async (id) => {
        await deletePhoto(id);
        cargarFotos();
    };

    return (
        <div style={{ padding: "20px" }}>

            <h2>CRUD Simple Fotos</h2>

            <input
                placeholder="Nombre"
                value={photo.nombre}
                onChange={(e) =>
                    setPhoto({ ...photo, nombre: e.target.value })
                }
            />

            <br />

            <input
                placeholder="Descripción"
                value={photo.descripcion}
                onChange={(e) =>
                    setPhoto({ ...photo, descripcion: e.target.value })
                }
            />

            <br />

            <input
                placeholder="URL"
                value={photo.url}
                onChange={(e) =>
                    setPhoto({ ...photo, url: e.target.value })
                }
            />

            <br />

            <button onClick={guardar}>
                {photo.idPhoto === 0 ? "Guardar" : "Actualizar"}
            </button>

            <hr />

            <table border="1" cellPadding="10">

                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Imagen</th>
                        <th>Acciones</th>
                    </tr>
                </thead>

                <tbody>

                    {photos.map((item) => (

                        <tr key={item.idPhoto}>

                            <td>{item.nombre}</td>

                            <td>{item.descripcion}</td>

                            <td>
                                <img
                                    src={item.url}
                                    width="100"
                                    alt={item.url}
                                />
                            </td>

                            <td>

                                <button
                                    onClick={() => editar(item)}
                                >
                                    Editar
                                </button>

                                <button
                                    onClick={() => eliminar(item.idPhoto)}
                                >
                                    Eliminar
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>
    );
}

export default App;