<template>
    <div class="container">
      <h2>Медикаменты</h2>
      <table class="table table-striped">
        <thead>
          <tr>
            <th>Название</th>
            <th>Дозировка</th>
            <th>Время приёма</th>
            <th>Действия</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="medication in medications" :key="medication.id">
            <td>{{ medication.name }}</td>
            <td>{{ medication.dosageValue }} {{ medication.dosageUnit }}</td>
            <td>{{ medication.intakeTime }}</td>
            <td>
              <button class="btn btn-sm btn-primary" @click="editMedication(medication)">Редактировать</button>
              <button class="btn btn-sm btn-danger" @click="deleteMedication(medication.id)">Удалить</button>
            </td>
          </tr>
        </tbody>
      </table>
      <button class="btn btn-success" @click="openAddForm">Добавить медикамент</button>
  
      <!-- Modal for Add/Edit -->
      <div v-if="showForm" class="modal" style="display: block;">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">{{ isEditing ? 'Редактировать медикамент' : 'Добавить медикамент' }}</h5>
              <button type="button" class="close" @click="closeForm">×</button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="saveMedication">
                <div class="form-group">
                  <label>Название</label>
                  <input v-model="currentMedication.name" class="form-control" required />
                </div>
                <div class="form-group">
                  <label>Дозировка</label>
                  <input v-model.number="currentMedication.dosageValue" type="number" class="form-control" step="any" required />
                </div>
                <div class="form-group">
                  <label>Единица дозировки</label>
                  <input v-model="currentMedication.dosageUnit" class="form-control" required />
                </div>
                <div class="form-group">
                  <label>Дата начала</label>
                  <input v-model="currentMedication.startDate" type="date" class="form-control" required />
                </div>
                <div class="form-group">
                  <label>Дата окончания</label>
                  <input v-model="currentMedication.endDate" type="date" class="form-control" />
                </div>
                <div class="form-group">
                  <label>Время приёма</label>
                  <input v-model="currentMedication.intakeTime" type="time" class="form-control" required />
                </div>
                <div class="form-group">
                  <label>Повторять каждые N дней</label>
                  <input v-model.number="currentMedication.repeatEveryNDays" type="number" class="form-control" min="1" required />
                </div>
                <div class="form-group">
                  <label>Примечания</label>
                  <textarea v-model="currentMedication.notes" class="form-control"></textarea>
                </div>
                <button type="submit" class="btn btn-primary">Сохранить</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { getMedications, createMedication, updateMedication, deleteMedication } from '../services/medicationService';
  
  export default {
    data() {
      return {
        medications: [],
        showForm: false,
        isEditing: false,
        currentMedication: {
          id: null,
          name: '',
          dosageValue: 0,
          dosageUnit: '',
          startDate: '',
          endDate: null,
          intakeTime: '',
          repeatEveryNDays: 1,
          notes: ''
        }
      };
    },
    async created() {
      await this.fetchMedications();
    },
    methods: {
      async fetchMedications() {
        try {
          const response = await getMedications();
          this.medications = response.items || [];
        } catch (error) {
          console.error('Ошибка при загрузке медикаментов:', error);
        }
      },
      openAddForm() {
        this.isEditing = false;
        this.currentMedication = {
          id: null,
          name: '',
          dosageValue: 0,
          dosageUnit: '',
          startDate: '',
          endDate: null,
          intakeTime: '',
          repeatEveryNDays: 1,
          notes: ''
        };
        this.showForm = true;
      },
      editMedication(medication) {
        this.isEditing = true;
        this.currentMedication = { ...medication, endDate: medication.endDate || null };
        this.showForm = true;
      },
      async saveMedication() {
        try {
          const medicationToSend = {
            ...this.currentMedication,
            startDate: this.currentMedication.startDate ? `${this.currentMedication.startDate}T00:00:00` : null,
            endDate: this.currentMedication.endDate ? `${this.currentMedication.endDate}T00:00:00` : null,
          };
          if (this.isEditing) {
            await updateMedication(this.currentMedication.id, medicationToSend);
          } else {
            const medicationData = { ...medicationToSend };
            delete medicationData.id;
            await createMedication(medicationData);
          }
          this.showForm = false;
          await this.fetchMedications();
        } catch (error) {
          console.error('Ошибка при сохранении медикамента:', error);
        }
      },
      async deleteMedication(id) {
        if (confirm('Вы уверены, что хотите удалить этот медикамент?')) {
          try {
            await deleteMedication(id);
            await this.fetchMedications();
          } catch (error) {
            console.error('Ошибка при удалении медикамента:', error);
          }
        }
      },
      closeForm() {
        this.showForm = false;
      }
    }
  };
  </script>
  
  <style scoped>
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }
  </style>