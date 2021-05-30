<template>
  <div>
    <div class="modal "
         id="keepDetailsModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content row">
          <div class="col-6"></div>

          <div class="col-6">
            <div class="row">
              <button type="button" class="btn btn-none text-danger" title="close" aria="close" data-dismiss="modal">
                <i class="fas fa-times"></i>
              </button>
            </div>

            <div class="row">
              {{ keep.name }}
            </div>
            <div class="row">
              {{ keep.description }}
            </div>
            <div class="row justify-content-center">
              {{ keep.views }}<i class="far fa-eye"></i>
              {{ keep.keeps }} <i class="fab fa-kaggle"></i>
              {{ keep.shares }}<i class="fas fa-share"></i>
            </div>
            <div class="row justify-content-around">
              <div class="col-5 text-left">
              </div>
              <div class="col-5 text-right"></div>
              Keep add to vault, keep creator router link and delete button
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { keepsService } from '../services/KeepService'
import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
export default {
  name: 'KeepDetailsModal',
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
          $('#keepDetailsModal').modal('hide')
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
