<template>
  <div class="card vaultcard my-2" style="max-width:20vw;">
    <router-link style="color: inherit;" :to="{name: 'Vaults', params: {id: vault.id}}" @click="activeVault(vault.id)">
      <div class="row justify-content-between">
        <h4 class="position-absolute m-2">
          {{ vault.name }}
        </h4>
        <img v-if="vault.img" class=" img-fluid rounded vaultimg" :src="vault.img " alt="Vault Image" style="max-width:20vw;">
      </div>
    </router-link>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Vault',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    })
    return {
      state,
      async activeVault(id) {
        try {
          await vaultsService.activeVault(id)
        } catch (error) {
          console.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
img{
  width: 100%;
}
.vaultimg{
   overflow-x: hidden;
   overflow-y: hidden;
}
</style>
