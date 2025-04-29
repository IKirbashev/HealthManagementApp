import api from './api';

export const getMedications = async (page = 1, pageSize = 10) => {
  const response = await api.get('/medications', {
    params: { page, pageSize },
  });
  return response.data;
};

export const getMedicationById = async (id) => {
  const response = await api.get(`/medications/${id}`);
  return response.data;
};

export const createMedication = async (medication) => {
  const response = await api.post('/medications', medication);
  return response.data;
};

export const updateMedication = async (id, medication) => {
  await api.put(`/medications/${id}`, medication);
};

export const deleteMedication = async (id) => {
  await api.delete(`/medications/${id}`);
};