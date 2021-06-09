<template>
  <div>
    <div class="modal "
         id="newKeepModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              CREATE A KEEP
            </h5>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createKeep">
              <div class="form-group">
                <label for="name">Title</label>
                <input type="text"
                       class="form-control"
                       id="name"
                       placeholder="Title..."
                       minlength="3"
                       maxlength="500"
                       v-model="state.newKeep.name"
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
                       v-model="state.newKeep.description"
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
                       v-model="state.newKeep.img"
                >
              </div>

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
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
export default {
  name: 'NewKeepModal',
  setup() {
    const state = reactive({
      newKeep: {},
      account: computed(() => AppState.account)
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          state.newKeep = {}
          $('#newKeepModal').modal('hide')
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
