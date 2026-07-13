import axios from "axios"

const Api = "https://localhost:7124/api/Foto";

export const getPhotos = async ()=> {
    const response = await axios.get(`${Api}/GetPhotos`);
    return response.data;
}

export const createPhoto = async (photo) => {
  const response = await axios.post(`${Api}/Create`, photo);
    return response.data;
}

export const updatePhoto = async (photo) => {
    const response = await axios.post(`${Api}/UpdatePhotos`, photo);
    return response.data;
}

export const deletePhoto = async (id) => {
    const response = await axios.delete(`${Api}/DeletePhotos?id=${id}`);
    return response.data;
}