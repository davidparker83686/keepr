<template>
  <label for="toggle_button">
    <span v-if="isActive">On</span>
    <span v-if="! isActive">Off</span>

    <input type="checkbox" id="toggle_button">
  </label>
</template>

<script>
export default {
  data() {
    return {
      currentState: false
    }
  },

  computed: {
    isActive() {
      return this.currentState
    }
  }
}
</script>
