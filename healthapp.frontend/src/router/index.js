import { createRouter, createWebHistory } from 'vue-router';
import HealthRecordsView from '../views/HealthRecordsView.vue';
import BiomarkersView from '../views/BiomarkersView.vue';
import MedicationsView from '../views/MedicationsView.vue';
import DocumentsView from '../views/DocumentsView.vue';

const routes = [
  { path: '/', redirect: '/health-records' },
  { path: '/health-records', component: HealthRecordsView },
  { path: '/biomarkers', component: BiomarkersView },
  { path: '/medications', component: MedicationsView },
  { path: '/documents', component: DocumentsView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;