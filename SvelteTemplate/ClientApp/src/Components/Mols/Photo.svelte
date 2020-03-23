<script>
  import { reglament } from "../../store.js";
  export let mol = null;
  export let save = null;

  function preview(e) {
    if (e.target.files && e.target.files[0]) {
      var reader = new FileReader();
      reader.onload = function(e) {
        mol.PHOTO_TYPE = e.target.result.split(",")[0];
        mol.PHOTO_IMAGE = e.target.result.split(",")[1];
      };
      reader.readAsDataURL(e.target.files[0]);
    }
  }
</script>

{#if mol.PHOTO_IMAGE}
  <img
    width="100%"
    class="mb-3"
    src="data:image/jpeg;base64, {mol.PHOTO_IMAGE}"
    alt="" />
{/if}

<form
  on:submit|preventDefault={x => save(mol)}
  on:reset={e => alert()}
  id="commonForm">

  <div class="form-group">
    <div class="custom-file">
      <input
        type="file"
        class="custom-file-input"
        id="customFile"
        accept="image/*"
        on:change={preview} />
      <label class="custom-file-label" for="customFile">Выберите файл</label>
    </div>

  </div>

  {#if mol.PHOTO_IMAGE}
    <button
      type="button"
      class="btn btn-danger"
      on:click={x => (mol.PHOTO_IMAGE = null)}>
      Удалить
    </button>
  {/if}
  <button
    type="submit"
    class:disabled={!$reglament.allowEdit}
    class="btn btn-primary">
    Сохранить
  </button>

</form>
