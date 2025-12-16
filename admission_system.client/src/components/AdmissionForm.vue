<script setup lang="ts">
  import { ref, watch } from 'vue';
  import axios, { AxiosError } from 'axios';

  interface FormData {
    age: number,
    visitor_type: string,
    isPass: boolean,
    pass_level: number,
    isWeapon: boolean,
    isAggressive: boolean,
    zone: string
  }

  const formData = ref<FormData>({
    age: 0,
    visitor_type: '',
    isPass: false,
    pass_level: 0,
    isWeapon: false,
    isAggressive: false,
    zone: ''
  });

  const responseMessage = ref<string>('');
  const error = ref<string>('');
  const isLoading = ref<boolean>(false);
  const validationErrors = ref<Record<string, string>>({});

  watch(() => formData.value.visitor_type, (newType) => {
    if (newType === 'guest') {
      formData.value.pass_level = 1;
    } else if (newType === 'employee') {
      formData.value.pass_level = 0;
    }
    if (validationErrors.value.visitor_type) {
      delete validationErrors.value.visitor_type;
    }
  });

  watch(() => formData.value.age, () => {
    if (validationErrors.value.age) {
      delete validationErrors.value.age;
    }
  });

  watch(() => formData.value.pass_level, () => {
    if (validationErrors.value.pass_level) {
      delete validationErrors.value.pass_level;
    }
  });

  watch(() => formData.value.zone, () => {
    if (validationErrors.value.zone) {
      delete validationErrors.value.zone;
    }
  });

  const validateForm = (): boolean => {
    validationErrors.value = {};
    let isValid = true;

    if (!formData.value.age || formData.value.age <= 0) {
      validationErrors.value.age = 'Please enter a valid age';
      isValid = false;
    }

    if (!formData.value.visitor_type) {
      validationErrors.value.visitor_type = 'Please select visitor type';
      isValid = false;
    }

    if (formData.value.visitor_type === 'employee') {
      if (!formData.value.pass_level || formData.value.pass_level === 0) {
        validationErrors.value.pass_level = 'Please select pass level';
        isValid = false;
      }
    }

    if (!formData.value.zone) {
      validationErrors.value.zone = 'Please select a zone';
      isValid = false;
    }

    return isValid;
  };

  const submitForm = async () => {
    responseMessage.value = '';
    error.value = '';

    if (!validateForm()) {
      error.value = 'Please fill in all required fields correctly';
      return;
    }

    isLoading.value = true;

    const passLevel = formData.value.visitor_type === 'guest' ? 1 : formData.value.pass_level;

    const requestData = {
      VisitorId: '00000000-0000-0000-0000-000000000000',
      Age: formData.value.age,
      VisitorType: formData.value.visitor_type,
      IsPass: formData.value.isPass,
      PassLevel: passLevel,
      IsWeapons: formData.value.isWeapon,
      IsAggressive: formData.value.isAggressive,
      Zone: formData.value.zone
    };

    console.log('Request: ' + requestData);

    try {
      const response = await axios.post<string>('https://localhost:7077/api/Admission/formCheck', requestData);
      responseMessage.value = response.data;
      console.log('Response:', response.data);
    } catch (err) {
      if (axios.isAxiosError(err)) {
        const axiosError = err as AxiosError;
        error.value = 'Failed to submit data: ' + (axiosError.response?.data || axiosError.message);
      } else {
        error.value = 'An unexpected error occurred.';
      }
    } finally {
      isLoading.value = false;
    }
  };
</script>

<template>
  <div class="admission-form">
    <h2 class="form-title">Admission Form</h2>

    <div class="form-group">
      <label class="form-label">Age <span class="required">*</span></label>
      <input
        v-model="formData.age"
        type="number"
        placeholder="Enter your age"
        :class="['form-input', { 'input-error': validationErrors.age }]"
      />
      <div v-if="validationErrors.age" class="error-text">{{ validationErrors.age }}</div>
    </div>

    <div class="form-group">
      <label class="form-label">Visitor Type <span class="required">*</span></label>
      <div class="radio-group">
        <div class="radio-item">
          <input
            type="radio"
            id="guest"
            value="guest"
            v-model="formData.visitor_type"
            class="radio-input"
          />
          <label for="guest" class="radio-label">Guest</label>
        </div>
        <div class="radio-item">
          <input
            type="radio"
            id="employee"
            value="employee"
            v-model="formData.visitor_type"
            class="radio-input"
          />
          <label for="employee" class="radio-label">Employee</label>
        </div>
      </div>
      <div v-if="validationErrors.visitor_type" class="error-text">{{ validationErrors.visitor_type }}</div>
    </div>

    <div class="form-group">
      <div class="checkbox-item">
        <input
          type="checkbox"
          id="isPass"
          v-model="formData.isPass"
          class="checkbox-input"
        />
        <label for="isPass" class="checkbox-label">
          Do you have a pass?
        </label>
      </div>
    </div>

    <div v-if="formData.visitor_type === 'employee'" class="form-group">
      <label class="form-label">Pass Level <span class="required">*</span></label>
      <select
        v-model="formData.pass_level"
        :class="['form-select', { 'input-error': validationErrors.pass_level }]"
      >
        <option disabled value="0">Please select Pass level</option>
        <option value="1">Level 1</option>
        <option value="2">Level 2</option>
        <option value="3">Level 3</option>
      </select>
      <div v-if="validationErrors.pass_level" class="error-text">{{ validationErrors.pass_level }}</div>
    </div>

    <div class="form-group">
      <div class="checkbox-item">
        <input
          type="checkbox"
          id="isWeapon"
          v-model="formData.isWeapon"
          class="checkbox-input"
        />
        <label for="isWeapon" class="checkbox-label">
          Do you have a weapon?
        </label>
      </div>
    </div>

    <div class="form-group">
      <div class="checkbox-item">
        <input
          type="checkbox"
          id="isAggressive"
          v-model="formData.isAggressive"
          class="checkbox-input"
        />
        <label for="isAggressive" class="checkbox-label">
          Are you aggressive?
        </label>
      </div>
    </div>

    <div class="form-group">
      <label class="form-label">Zone <span class="required">*</span></label>
      <select
        v-model="formData.zone"
        :class="['form-select', { 'input-error': validationErrors.zone }]"
      >
        <option disabled value="">Please select zone</option>
        <option value="Meeting Room">Meeting Room</option>
        <option value="Laboratory">Laboratory</option>
        <option value="Secret Laboratory">Secret Laboratory</option>
      </select>
      <div v-if="validationErrors.zone" class="error-text">{{ validationErrors.zone }}</div>
    </div>

    <!-- Result display section -->
    <div v-if="responseMessage" class="result-message success">
      {{ responseMessage }}
    </div>

    <div v-if="error" class="result-message error-message">
      {{ error }}
    </div>

    <button @click="submitForm" class="submit-button" :disabled="isLoading">
      {{ isLoading ? 'Submitting...' : 'Submit Application' }}
    </button>
  </div>
</template>

<style scoped>
.admission-form {
  max-width: 600px;
  margin: 2rem auto;
  padding: 2rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  border-radius: 12px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
}

.form-title {
  text-align: center;
  color: white;
  margin-bottom: 2rem;
  font-size: 2rem;
  font-weight: 600;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
}

.form-group {
  margin-bottom: 1.5rem;
  background: white;
  padding: 1.25rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.form-label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #333;
  font-size: 1rem;
}

.form-input {
  width: 100%;
  padding: 0.75rem;
  border: 2px solid #e0e0e0;
  border-radius: 6px;
  font-size: 1rem;
  transition: border-color 0.3s ease;
  box-sizing: border-box;
}

.form-input:focus {
  outline: none;
  border-color: #667eea;
}

.form-select {
  width: 100%;
  padding: 0.75rem;
  border: 2px solid #e0e0e0;
  border-radius: 6px;
  font-size: 1rem;
  background: white;
  cursor: pointer;
  transition: border-color 0.3s ease;
  box-sizing: border-box;
}

.form-select:focus {
  outline: none;
  border-color: #667eea;
}

.form-value {
  margin-top: 0.5rem;
  font-size: 0.9rem;
  color: #666;
  font-style: italic;
}

.radio-group {
  display: flex;
  gap: 1.5rem;
  margin-bottom: 0.5rem;
}

.radio-item {
  display: flex;
  align-items: center;
}

.radio-input {
  width: 18px;
  height: 18px;
  margin-right: 0.5rem;
  cursor: pointer;
  accent-color: #667eea;
}

.radio-label {
  cursor: pointer;
  color: #333;
  font-size: 1rem;
}

.checkbox-item {
  display: flex;
  align-items: center;
}

.checkbox-input {
  width: 20px;
  height: 20px;
  margin-right: 0.75rem;
  cursor: pointer;
  accent-color: #667eea;
}

.checkbox-label {
  cursor: pointer;
  color: #333;
  font-size: 1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.checkbox-value {
  font-weight: 600;
  color: #667eea;
}

.submit-button {
  width: 100%;
  padding: 1rem;
  background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.submit-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
}

.submit-button:active {
  transform: translateY(0);
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.submit-button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
  transform: none;
}

.result-message {
  margin-bottom: 1.5rem;
  padding: 1rem;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  text-align: center;
  animation: fadeIn 0.3s ease-in;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.result-message.success {
  background: linear-gradient(135deg, #84fab0 0%, #8fd3f4 100%);
  color: #006400;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.result-message.error-message {
  background: linear-gradient(135deg, #ff9a9e 0%, #fecfef 100%);
  color: #8b0000;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* Validation styles */
.required {
  color: #f5576c;
  font-weight: bold;
  margin-left: 0.25rem;
}

.input-error {
  border-color: #f5576c !important;
  background-color: #fff5f5;
}

.input-error:focus {
  border-color: #f5576c !important;
  box-shadow: 0 0 0 3px rgba(245, 87, 108, 0.1);
}

.error-text {
  margin-top: 0.5rem;
  color: #f5576c;
  font-size: 0.875rem;
  font-weight: 500;
  display: flex;
  align-items: center;
  gap: 0.25rem;
}

.error-text::before {
  content: '⚠';
  font-size: 1rem;
}
</style>
