<script>
  import { onMount} from 'svelte'
  import { asLoader, asDate} from '../../Shared/Actions.js'
  import Modal from '../../Shared/Modal.svelte'
  import {toAsts} from '../../store.js'
  import PickerField from '../../Shared/Forms/PickerField.svelte'
  import DateField from '../../Shared/Forms/DateField.svelte'
  
  

  export let mol = null;
  export let posts = [];
  export let depts = [];

  let data = []
  let loading;

  export let refresh;
  
  let getData = async () => {
    loading = true
    let q = await fetch(`/api/mols/${mol.MOL_ID}/hist`);
    data = await q.json()
    console.log(data)
    loading = false
  }


  onMount(getData)

  let modal;

  let actionNewPost = {molId: mol.MOL_ID, dFrom: new Date(), deptId: mol.DDEPT_ID, postId: mol.POST_ID};
  let newPostQuery = async () => {

    try {
        let q = await fetch(`/api/mols/newpostquery`, {
          method: 'post',
          headers: {'Content-Type': 'application/json'},
          body: JSON.stringify(actionNewPost)
        })    

        if (q.ok) {
          modal.hide()
          await getData();
          refresh();
        }
        else {
          toAsts.error(q.statusText)  
        }
      
    } catch (error) {
      toAsts.error(error.message)  
    }

  }
  
</script>
  
  <h5 use:asLoader={loading}>История назначений</h5>

  <table class="table table-sm">
    <tbody>
        {#each data as item}
            <tr>
                <td use:asDate={item.D_FROM}></td>
                <td>{item.POST_NAME}</td>
                <td>{item.DEPT_NAME}</td>
            </tr>
        {/each}
    </tbody>
  </table>


  <button class="btn btn-outline-primary" on:click={modal.show}>Новое назначение...</button>
  
  <Modal bind:modal>
    <div slot="header">Новое назначение</div>

    <form id="newPostForm" on:submit|preventDefault={newPostQuery}>

    <div class="form-row">
    <div class="form-group col-md-6">
        <DateField text="Дата назначения" bind:value={actionNewPost.dFrom} required></DateField>
    </div>
    </div>
    <div class="form-group">
      <PickerField text="Отдел" list={depts} bind:value={actionNewPost.deptId} required/>
    </div>
    <div class="form-group">
      <PickerField text="Должность" list={posts} bind:value={actionNewPost.postId} required/>
    </div>


    </form>
    


    <div slot="footer">
      <button class="btn btn-outline-primary" form="newPostForm">Назначить</button>
    </div>
  </Modal>

  

