<template>
  <div class="vaultspage container-fluid m-0 p-0s">
    <!-- Profile info -->
    <div class="row">
      <div class="col-12 m-5">
        <h3 class="d-inline">
          {{ state.activeVault.name }}
        </h3>
        <!-- <div v-if="state.activeVault.creatorId== state.account.id"> -->

        <button v-if="state.activeVault.creatorId == state.account.id"
                type="button"
                title="Delete Vault"
                aria-label="Delete Vault"
                class=" btn mx-3 p-0 d-inline btn-none shadow-none border-none outline-none text-danger"
                @click="deleteVault(state.activeVault.id)"
        >
          <i class="fas fa-trash-alt"></i>
        </button>
      </div>
    </div>
    <div class="col-12">
      <!-- <span> Keeps:{{ state.keeps.length }}</span> -->
    </div>
  </div>
  <div class="row">
    <div class="col-12 " v-if="keeps">
      <Keep v-for="keep in state.vaultkeeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
  <!-- </div> -->
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import Notification from '../utils/Notification'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultsPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      // const route = useRoute(),
      keep: computed(() => AppState.keeps),
      vaultkeeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault),
      activeAccount: computed(() => AppState.activeAccount),
      accountVaults: computed(() => AppState.accountVaults),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    // watch(() => state.loading, () => {
    //   accountService.getActive(route.params.id)
    // })
    onMounted(async() => {
      try {
        await keepsService.getKeepsByUserId(route.params.id)
        await vaultsService.getVaultsByUserId(route.params.id)
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getVaultKeepsById(route.params.id)
        await vaultsService.activeVault(route.params.id)
      } catch (error) {
        console.error(error)
      }
    })

    return {
      state,
      route,
      async deleteVault(id) {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this Vault?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await vaultsService.deleteVault(route.params.id)
            router.push({ name: 'Home' })
            // router.push({ name: 'Home', params: {id: state.activeKeep.creatorId })
            Notification.toast('Successfully Deleted Vault', 'success')
          }
        } catch (error) {
          console.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
