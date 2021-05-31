<template>
  <div class="vault container-fluid">
    <!-- Profile info -->
    <div class="row">
      <div class="col-12">
        {{ state.vault.title }}
        <div v-if="vault.creatorId== state.account.id">
          <button type="button" title="Delete Vault" aria-label="Delete Vault" class="btn btn-none text-danger" @click="deleteVault(vault.id)">
            <i class="fas fa-trash-alt"></i>
          </button>
        </div>
      </div>
      <div class="col-12">
        <span> Keeps:{{ state.keeps.length }}</span>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { accountService } from '../services/AccountService'
import { keepService } from '../services/KeepService'
import { vaultService } from '../services/VaultService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      keep: computed(() => AppState.keeps),
      vault: computed(() => AppState.vaults),
      activeAccount: computed(() => AppState.activeAccount),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    // watch(() => state.loading, () => {
    //   accountService.getActive(route.params.id)
    // })
    onMounted(async() => {
      try {
        if (!state.loading) {
          await accountService.getActive(route.params.id)
        }
        await keepService.getKeepsByUserId(route.params.id)
        await vaultService.getVaultsByUserId(route.params.id)
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
            await vaultService.deleteVault(id)
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
