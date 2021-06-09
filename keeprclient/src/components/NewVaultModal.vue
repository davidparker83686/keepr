<template>
  <div>
    <div class="modal "
         id="newVaultModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              CREATE A VAULT
            </h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault">
              <div class="form-group">
                <label for="name">Title</label>
                <input type="text"
                       class="form-control"
                       id="name"
                       placeholder="Title..."
                       minlength="3"
                       maxlength="100"
                       v-model="state.newVault.name"
                       required
                >
              </div>
              <div class="form-group">
                <label for="description">Description</label>
                <input type="text"
                       class="form-control"
                       id="description"
                       placeholder="Description..."
                       minlength="3"
                       maxlength="2000"
                       v-model="state.newVault.description"
                >
              </div>
              <div class="form-group">
                <label for="img">Image URL</label>
                <input type="text"
                       class="form-control"
                       id="img"
                       placeholder="Image..."
                       minlength="3"
                       maxlength="500"
                       v-model="state.newVault.img"
                >
              </div>

              <!-- <div class="form-group form-check">
                <label class="form-check-label" for="isPrivate">Make Private</label>
                <input type="checkbox"
                       class="form-check-input"
                       id="isPrivate"
                       v-model="state.newVault.isPrivate"
                >
              </div> -->

              <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">
                  Dismiss
                </button>
                <button type="submit" class="btn btn-primary">
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
export default {
  name: 'NewVaultModal',
  setup() {
    const state = reactive({
      newVault: {},
      account: computed(() => AppState.account)
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#newVaultModal').modal('hide')
        } catch (error) {
          logger.error(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style scoped>
</style>
