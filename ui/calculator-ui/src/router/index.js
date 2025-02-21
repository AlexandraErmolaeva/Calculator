import { createRouter, createWebHistory } from 'vue-router';
import ArrayCalculator from '../components/ArrayCalculator.vue';
import ExpressionCalculator from '../components/ExpressionCalculator.vue';

const routes = [
  {
    path: '/',
    name: 'ArrayCalculator',
    component: ArrayCalculator
  },
  {
    path: '/expression',
    name: 'ExpressionCalculator',
    component: ExpressionCalculator
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;